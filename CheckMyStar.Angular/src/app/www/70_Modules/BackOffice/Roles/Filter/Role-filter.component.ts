import { Component, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { RoleFilter } from '../../../../30_Filters/BackOffice/role.filter';
import { FieldComponent } from '../../../Components/Field/Field.component';

@Component({
	selector: 'app-role-filter',
	standalone: true,
	imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent],
	templateUrl: './Role-filter.component.html'
})
export class RoleFilterComponent {
    filter = output<RoleFilter>();
    form: FormGroup;
	constructor(private fb: FormBuilder) { 
        this.form = this.fb.group({
        name: ['']
      });
	}
    search(): void {
        const filters = {...this.form.value};
        if (!filters.name) {
            return;
        }
        this.filter.emit(filters);
    }
    reset(): void {
        this.form.reset();
  }
}
