import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
  selector: 'app-front-quotes-page',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, TranslationModule],
  templateUrl: './Quotes-page.component.html',
  styleUrls: ['./Quotes-page.component.css']
})
export class FrontQuotesPageComponent {

  quoteForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.quoteForm = this.fb.group({
      quoteNumber:         ['', Validators.required],
      quoteDate:           ['', Validators.required],
      clientName:          ['', Validators.required],
      clientAddress:       ['', Validators.required],
      companyName:         ['', Validators.required],
      companyAddress:      ['', Validators.required],
      servicesDescription: ['', Validators.required],
      quantity:            ['', Validators.required],
      unitPrice:           ['', Validators.required],
      vatRate:             ['', Validators.required],
      totalHT:             ['', Validators.required],
      totalTTC:            ['', Validators.required],
      additionalFees:      [''],
      validityPeriod:      ['', Validators.required],
      executionDate:       [''],
      status:              ['', Validators.required]
    });
  }

  isInvalid(controlName: string): boolean {
    const control = this.quoteForm.get(controlName);
    return !!(control && control.invalid && control.touched);
  }
}
