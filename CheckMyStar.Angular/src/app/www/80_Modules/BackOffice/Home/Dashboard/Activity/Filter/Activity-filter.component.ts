import { Component, output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TranslationModule } from '../../../../../../10_Common/Translation.module';
import { ActivityFilter } from '../../../../../../30_Filters/BackOffice/Activity-filter';
import { FieldComponent } from '../../../../../Components/Field/Field.component';
import { FilterComponent } from '../../../../../Components/Filter/Filter.component';

@Component({
	selector: 'app-activity-filter',
	standalone: true,
	imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, FilterComponent],
	templateUrl: './Activity-filter.component.html'
})
export class ActivityFilterComponent {
    @Input() loadingSearch = false; 
    @Input() loadingReset = false;

    filter = output<ActivityFilter>({ alias: 'filter' });
    form: FormGroup;
	constructor(private fb: FormBuilder) { 
        this.form = this.fb.group({
        lastName: [''],
        firstName: [''],
        description: [''],
        createdDate: [''],
        isSuccess: [null]
      });
	}
    search(): void {
        const filters = {...this.form.value};
        this.filter.emit(filters);
    }
    reset(): void {
        this.form.reset();
        this.filter.emit({ reset: true });
  }
}
