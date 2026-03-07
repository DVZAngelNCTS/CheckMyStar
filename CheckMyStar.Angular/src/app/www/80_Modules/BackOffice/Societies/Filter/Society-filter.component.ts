import { Component, output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { SocietyFilter } from '../../../../30_Filters/BackOffice/Society.filter';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { FilterComponent } from '../../../Components/Filter/Filter.component';

@Component({
	selector: 'app-society-filter',
	standalone: true,
	imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, FilterComponent],
	templateUrl: './Society-filter.component.html'
})
export class SocietyFilterComponent {
    @Input() loadingSearch = false; 
    @Input() loadingReset = false;

    filter = output<SocietyFilter>({ alias: 'filter' });
    form: FormGroup;
	constructor(private fb: FormBuilder) { 
        this.form = this.fb.group({
        name: [''],
        email: [''],
        phone: [''],
        address: ['']
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
