import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddressFilterComponent } from './Filter/Address-filter.component';
import { AddressBllService } from '../../../60_Bll/BackOffice/Address-bll.service';
import { AddressModel } from '../../../20_Models/Common/Address.model';
import { TableColumn } from '../../../80_Modules/Components/Table/Models/TableColumn.model'
import { TableComponent } from '../../Components/Table/Table.component'
import { TranslationModule } from '../../../10_Common/Translation.module';
import { PopupComponent } from '../../Components/Popup/Popup.component';
import { TranslateService } from '@ngx-translate/core';
import { ToastService } from '../../../90_Services/Toast/Toast.service';
import { CountryBllService } from '../../../60_Bll/BackOffice/Country-bll.service';
import { CountryModel } from '../../../20_Models/Common/Country.model';
import { AddressFormComponent } from '../Addresses/Form/Address-form.component';

@Component({
	selector: 'app-address-page',
	standalone: true,
	imports: [CommonModule, AddressFilterComponent, FormsModule, ReactiveFormsModule, TableComponent,TranslationModule, PopupComponent, AddressFormComponent],
	templateUrl: './Address-page.component.html'
})
export class AddressPageComponent {
	popupVisible = false; 
	popupMode: 'create' | 'edit' | 'delete' | null = null;
	popupTitle = ''; 
	popupConfirmLabel = ''; 
	popupCancelLabel = ''; 
	selectedAddress: AddressModel | null = null;
	@ViewChild(AddressFormComponent) addressForm!: AddressFormComponent;
	popupError: string | null = null;	
	addresses?: AddressModel[] = [];
	loading = false;
	loadingSearch = false; 
	loadingReset = false;
    countries: CountryModel[] = [];

	columns = [
		{ icon: 'bi bi-list-ol', field: 'identifier', header: 'AddressSection.Identifier', sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-1-square', field: 'number', header: 'AddressSection.Number', translate: true, sortable: true, filterable: true, width: '8%' },
		{ icon: 'bi bi-geo-alt', field: 'addressLine', header: 'AddressSection.AddressLine', translate: true, sortable: true, filterable: true },
		{ icon: 'bi bi-building', field: 'city', header: 'AddressSection.City', translate: true, sortable: true, filterable: true, width: '12%' },
		{ icon: 'bi bi-mailbox', field: 'zipCode', header: 'AddressSection.ZipCode', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-geo', field: 'region', header: 'AddressSection.Region', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-globe', field: 'country', header: 'AddressSection.Country', translate: true, sortable: true, filterable: true, width: '10%',
		 	pipe: (c) => { if (!c) return ''; return c.name ?? ''; }},

		] as TableColumn<AddressModel>[];

	constructor(private fb: FormBuilder, private translate: TranslateService, private toast: ToastService, private addressBll: AddressBllService, private countryBll: CountryBllService) { 
	}

	ngOnInit() {
		this.loadAddresses();
		this.loadCountries();
	}

	loadAddresses() {
		this.addressBll.getAddresses$().subscribe({
			next: addresses => this.addresses = addresses.addresses,
			error: err => console.error(err)
		});
	}

	loadCountries() {
		this.countryBll.getCountries$().subscribe({
		next: (response) => {
			this.countries = response.countries || [];
		},
		error: (err) => console.error('Erreur chargement pays', err)
        });
    }

	onFilter(filter: any) {

		if (filter.reset)
			this.loadingReset = true;
		else
			this.loadingSearch = true;

		this.addressBll.getAddresses$(filter.number, filter.addressLine, filter.city, filter.zipCode, filter.region, filter.countryIdentifier).subscribe({
			next: addresses => {
				if (filter.reset)
						this.loadingReset = false;
					else
						this.loadingSearch = false;
				this.addresses = addresses.addresses
			},
			error: err => {
				if (filter.reset)
						this.loadingReset = false;
					else
						this.loadingSearch = false;
				console.error(err)
			}
		});
	}

	openCreate() {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.popupMode = 'create';
		this.popupTitle = this.translate.instant('AddressSection.Create');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	openUpdate(address: AddressModel) {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedAddress = address;
		this.popupMode = 'edit';
		this.popupTitle = this.translate.instant('AddressSection.Update');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	openDelete(address: AddressModel) {		
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedAddress = address;
		this.popupMode = 'delete';
		this.popupTitle = this.translate.instant('AddressSection.Delete');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	onPopupConfirm() {
		if (this.popupMode === 'create') {
			if (this.addressForm.form.invalid) {
			this.addressForm.form.markAllAsTouched();
			return; // ❗ NE PAS FERMER LA POPUP
			}
			this.onCreateConfirmed();
			return;
		}

		if (this.popupMode === 'edit') {
			if (this.addressForm.form.invalid) {
			this.addressForm.form.markAllAsTouched();
			return; // ❗ NE PAS FERMER LA POPUP
			}
			this.onEditConfirmed();
			return;
		}

		if (this.popupMode === 'delete') {
			this.onDeleteConfirmed();
			return;
		}

		// 👉 Ici seulement si tout est OK
		this.popupVisible = false;
	}

	onPopupCancel() {
		this.popupVisible = false;
	}

	onCreateConfirmed() {
		if (this.addressForm.form.invalid) {
			this.addressForm.form.markAllAsTouched();
			return;
		}

		this.loading = true;

		const newAddress = this.addressForm.getValue();

		this.addressBll.addAddress$(newAddress).subscribe({
			next: response => {
					if (!response.isSuccess) {
						this.loading = false;
						this.popupError = response.message;
						return; // ❗ ne pas fermer la popup
					}

					this.popupError = null;
					this.loadAddresses();
					this.toast.show(response.message, "success", 5000);
					this.popupVisible = false;
				},
				error: err => {
					this.loading = false;
					this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
				}
		});
	}

	onEditConfirmed() {
		if (this.addressForm.form.invalid) {
			this.addressForm.form.markAllAsTouched();
			return;
		}

		this.loading = true;

		const updatedAddress = this.addressForm.getValue();

		this.addressBll.updateAddress$(updatedAddress).subscribe({
			next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;
					return;
				}

				this.popupError = null;
				this.loadAddresses();
				this.toast.show(response.message, "success", 5000);
				this.popupVisible = false;
			},
			error: err => {
				this.loading = false;
				this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
			}
		});
	}

	onDeleteConfirmed() {
		if (!this.selectedAddress) return;

		this.loading = true;

		this.addressBll.deleteAddress$(this.selectedAddress.identifier).subscribe({
			next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;
					return;
				}

				this.popupError = null;
				this.loadAddresses();		
				this.toast.show(response.message, "success", 5000);		
				this.popupVisible = false;
			},
			error: err => {
				this.loading = false;
				this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
			}
		});
	}
}