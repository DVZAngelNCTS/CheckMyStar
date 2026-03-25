import { Component, output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { QuoteFilter } from '../../../../30_Filters/BackOffice/Quote.filter';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { FilterComponent } from '../../../Components/Filter/Filter.component';

@Component({
  selector: 'app-front-quote-filter',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, FilterComponent],
  templateUrl: './Quote-filter.component.html'
})
export class FrontQuoteFilterComponent {
  @Input() loadingSearch = false;
  @Input() loadingReset = false;

  filter = output<QuoteFilter>({ alias: 'filter' });
  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      reference: [''],
      isAccepted: [null]
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
