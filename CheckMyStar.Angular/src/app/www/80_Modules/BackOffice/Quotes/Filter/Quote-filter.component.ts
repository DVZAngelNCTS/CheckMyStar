import { Component, Input, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { QuoteFilter } from '../../../../30_Filters/BackOffice/Quote.filter';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { FilterComponent } from '../../../Components/Filter/Filter.component';

@Component({
  selector: 'app-quote-filter',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, FilterComponent],
  templateUrl: './Quote-filter.component.html'
})
export class QuoteFilterComponent {
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
    this.filter.emit({ ...this.form.value });
  }

  reset(): void {
    this.form.reset();
    this.filter.emit({ reset: true });
  }
}