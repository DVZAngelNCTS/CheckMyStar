import { Component, output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { RoleFilter } from '../../../../30_Filters/BackOffice/Role.filter';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { FilterComponent } from '../../../Components/Filter/Filter.component';

@Component({
	selector: 'app-role-filter',
	standalone: true,
	imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, FilterComponent],
	templateUrl: './Role-filter.component.html'
})
export class RoleFilterComponent {
    @Input() loadingSearch = false; 
    @Input() loadingReset = false;

    filter = output<RoleFilter>({ alias: 'filter' });
    form: FormGroup;
	constructor(private fb: FormBuilder) { 
        this.form = this.fb.group({
        name: ['']
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
