import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormArray, FormBuilder, Validators } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { UserModel } from '../../../../20_Models/Common/User.model';
import { OwnerAutocompleteComponent } from '../../../Components/AutoCompletion/Owner-autocompletion.component';

@Component({
  selector: 'app-quote-create-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslationModule, OwnerAutocompleteComponent],
  templateUrl: './Quote-create-form.component.html',
  styleUrls: ['./Quote-create-form.component.css']
})
export class QuoteCreateFormComponent {

  @Input() quoteForm!: FormGroup;
  @Input() clients: UserModel[] = [];
  @Input() selectedClientIdentifier: number | null = null;
  @Output() clientSelected = new EventEmitter<number>();

  constructor(private fb: FormBuilder) {}

  get services(): FormArray {
    return this.quoteForm.get('services') as FormArray;
  }

  createService(): FormGroup {
    return this.fb.group({
      description: ['', Validators.required],
      quantity:    [1, [Validators.required, Validators.min(0)]],
      unit:        [''],
      unitPrice:   [0, [Validators.required, Validators.min(0)]]
    });
  }

  addService(): void {
    this.services.push(this.createService());
  }

  removeService(index: number): void {
    if (this.services.length > 1) {
      this.services.removeAt(index);
    }
  }

  get subTotalHT(): number {
    return this.services.controls.reduce((sum, ctrl) => {
      const q = +ctrl.get('quantity')?.value || 0;
      const p = +ctrl.get('unitPrice')?.value || 0;
      return sum + q * p;
    }, 0);
  }

  get totalHT(): number {
    return this.subTotalHT;
  }

  get totalTTC(): number {
    const rate = +this.quoteForm.get('vatRate')?.value || 0;
    return this.totalHT * (1 + rate / 100);
  }

  isInvalid(controlName: string): boolean {
    const control = this.quoteForm.get(controlName);
    return !!(control && control.invalid && control.touched);
  }

  isServiceInvalid(index: number, field: string): boolean {
    const control = this.services.at(index).get(field);
    return !!(control && control.invalid && control.touched);
  }

  get statusLabel(): string {
    const map: Record<string, string> = {
      draft: 'Brouillon', sent: 'Envoyé', accepted: 'Accepté', refused: 'Refusé'
    };
    return map[this.quoteForm.get('status')?.value] || '';
  }

  get statusClass(): string[] {
    const map: Record<string, string[]> = {
      draft:    ['bg-secondary', 'text-white'],
      sent:     ['bg-primary',   'text-white'],
      accepted: ['bg-success',   'text-white'],
      refused:  ['bg-danger',    'text-white']
    };
    return map[this.quoteForm.get('status')?.value] || [];
  }

  onClientSelected(clientIdentifier: number): void {
    this.clientSelected.emit(clientIdentifier);
    this.quoteForm.get('clientName')?.markAsTouched();
  }
}
