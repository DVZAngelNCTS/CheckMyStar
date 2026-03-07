import { Component, output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { UserFilter } from '../../../../30_Filters/BackOffice/User-filter';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { FilterComponent } from '../../../Components/Filter/Filter.component';

@Component({
	selector: 'app-address-filter',
	standalone: true,
	imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, FilterComponent],
	templateUrl: './Address-filter.component.html'
})
export class AddressFilterComponent {
    @Input() loadingSearch = false; 
    @Input() loadingReset = false;
    @Input() countries: any[] = []; 

    filter = output<UserFilter>({ alias: 'filter' });
    form: FormGroup;
	constructor(private fb: FormBuilder) { 
        this.form = this.fb.group({
        number: [''],
        addressLine: [''],
        city: [''],
        zipCode: [''],
        region: [null],
        countryIdentifier: ['']
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
