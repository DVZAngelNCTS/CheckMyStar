import { Component, Input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { InvoiceFilter } from '../../../../30_Filters/BackOffice/Invoice.filter';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { FilterComponent } from '../../../Components/Filter/Filter.component';

@Component({
  selector: 'app-invoice-filter',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, FilterComponent],
  templateUrl: './Invoice-filter.component.html'
})
export class InvoiceFilterComponent {
  @Input() loadingSearch = false;
  @Input() loadingReset = false;

  filter = output<InvoiceFilter>({ alias: 'filter' });
  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      invoiceNumber: [''],
      paymentStatusIdentifier: [null],
      isActive: [null]
    });
  }

  search(): void {
    this.filter.emit({ ...this.form.value });
  }

  reset(): void {
    this.form.reset();
    this.filter.emit({ reset: true });
  }
}