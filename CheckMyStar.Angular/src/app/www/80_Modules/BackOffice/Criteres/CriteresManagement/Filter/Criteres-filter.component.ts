import { Component, output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder } from '@angular/forms';
import { TranslationModule } from '../../../../../10_Common/Translation.module';
import { FieldComponent } from '../../../../Components/Field/Field.component';
import { MiniLoaderComponent } from '../../../../Components/Loader/Mini/Loader-mini.component';

@Component({
  selector: 'app-criteres-filter',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, MiniLoaderComponent],
  templateUrl: './Criteres-filter.component.html'
})
export class CriteresFilterComponent {
  @Input() loadingSearch = false; 
  @Input() loadingReset = false;

  filter = output<any>({ alias: 'filter' });
  add = output<void>();
  form: FormGroup;

  constructor(private fb: FormBuilder) { 
    this.form = this.fb.group({
      description: [''],
      typeCode: [''],
      basePoints: ['']
    });
  }

  search(): void {
    this.filter.emit(this.form.value);
  }

  reset(): void {
    this.form.reset();
    this.filter.emit({ reset: true });
  }
}
