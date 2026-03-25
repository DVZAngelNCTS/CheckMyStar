import { Component, output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { InvoiceFilter } from '../../../../30_Filters/BackOffice/Invoice.filter';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { FilterComponent } from '../../../Components/Filter/Filter.component';

@Component({
  selector: 'app-front-invoice-filter',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, FilterComponent],
  templateUrl: './Invoice-filter.component.html'
})
export class FrontInvoiceFilterComponent {
  @Input() loadingSearch = false;
  @Input() loadingReset = false;

  filter = output<InvoiceFilter>({ alias: 'filter' });
  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      invoiceNumber: [''],
      paymentStatusIdentifier: [null]
    });
  }

  search(): void {
    const filters = { ...this.form.value };
    this.filter.emit(filters);
  }

  reset(): void {
    this.form.reset();
    this.filter.emit({ reset: true });
  }
}
