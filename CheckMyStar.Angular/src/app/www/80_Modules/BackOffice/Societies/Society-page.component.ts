import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SocietyFilterComponent } from './Filter/Society-filter.component';
import { SocietyBllService } from '../../../60_Bll/BackOffice/Society-bll.service';
import { SocietyModel } from '../../../20_Models/BackOffice/Society.model';
import { TableColumn } from '../../../80_Modules/Components/Table/Models/TableColumn.model'
import { TableComponent } from '../../Components/Table/Table.component'
import { TranslationModule } from '../../../10_Common/Translation.module';
import { PopupComponent } from '../../Components/Popup/Popup.component';
import { SocietyFormComponent } from '../Societies/Form/Society-form.component'
import { TranslateService } from '@ngx-translate/core';
import { ToastService } from '../../../90_Services/Toast/Toast.service';

@Component({
	selector: 'app-society-page',
	standalone: true,
	imports: [CommonModule, SocietyFilterComponent, FormsModule, ReactiveFormsModule, TableComponent, TranslationModule, PopupComponent, SocietyFormComponent],
	templateUrl: './Society-page.component.html'
})
export class SocietyPageComponent {
	popupVisible = false; 
	popupMode: 'create' | 'edit' | 'delete' | null = null; 
	popupTitle = ''; 
	popupConfirmLabel = ''; 
	popupCancelLabel = ''; 
	selectedSociety: SocietyModel | null = null;
	@ViewChild(SocietyFormComponent) societyForm!: SocietyFormComponent;
	popupError: string | null = null;	
	societies?: SocietyModel[] = [];
	loading = false;
	loadingSearch = false; 
	loadingReset = false;

	columns = [
		{ icon: 'bi bi-list-ol', field: 'identifier', header: 'SocietySection.Identifier', sortable: true, filterable: true, width: '9%' },
		{ icon: 'bi bi-shield', field: 'name', header: 'SocietySection.Name', translate: true, sortable: true, filterable: true, width: '12%' },
		{ icon: 'bi bi-envelope-at', field: 'email', header: 'SocietySection.Email', translate: true, sortable: true, filterable: true, width: '12%' },
		{ icon: 'bi bi-telephone', field: 'phone', header: 'SocietySection.Phone', translate: true, sortable: true, filterable: true, width: '7%' },
		{ icon: 'bi bi-envelope', field: 'address', header: 'SocietySection.Address', translate: true, sortable: true, filterable: true,
		 	pipe: (a) => { if (!a) return ''; const line1 = [a.number, a.addressLine].filter(Boolean).join(' '); const line2 = [a.zipCode, a.city].filter(Boolean).join(' '); const country = a.country?.name ?? ''; return [line1, line2, country].filter(Boolean).join(', '); }},
		{ icon: 'bi bi-shield-check', field: 'isActive', header: 'SocietySection.Active', sortable: true, filterable: false, width: '10%' }
		] as TableColumn<SocietyModel>[];

	constructor(private societyBll: SocietyBllService, private translate: TranslateService, private toast: ToastService) { 
	}

	ngOnInit() {
		this.loadSocieties();
	}

	loadSocieties() {
		this.societyBll.getSocieties$().subscribe({
			next: societies => this.societies = societies.societies,
			error: err => console.error(err)
		});
	}

	onFilter(filter: any) {
		if (filter.reset)
			this.loadingReset = true;
		else
			this.loadingSearch = true;

		this.societyBll.getSocieties$(filter.name, filter.email, filter.phone, filter.address).subscribe({
			next: societies => {
				if (filter.reset)
						this.loadingReset = false;
					else
						this.loadingSearch = false;
				this.societies = societies.societies
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
		this.popupTitle = this.translate.instant('SocietySection.Create');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	openUpdate(society: SocietyModel) {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedSociety = society;
		this.popupMode = 'edit';
		this.popupTitle = this.translate.instant('SocietySection.Update');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	openDelete(society: SocietyModel) {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedSociety = society;
		this.popupMode = 'delete';
		this.popupTitle = this.translate.instant('SocietySection.Delete');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	onPopupConfirm() {

	if (this.popupMode === 'create') {
		if (this.societyForm.form.invalid) {
			this.societyForm.form.markAllAsTouched();
			return; // ❗ NE PAS FERMER LA POPUP
		}
		this.onCreateConfirmed();
		return;
	}

	if (this.popupMode === 'edit') {
		if (this.societyForm.form.invalid) {
			this.societyForm.form.markAllAsTouched();
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
	if (this.societyForm.form.invalid) {
		this.societyForm.form.markAllAsTouched();
		return;
	}

	this.loading = true;

	const newSociety = this.societyForm.getValue();

	this.societyBll.addSociety$(newSociety).subscribe({
		next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;					
					return;
				}

				this.popupError = null;
				this.loadSocieties();
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
		if (this.societyForm.form.invalid) {
			this.societyForm.form.markAllAsTouched();
			return;
		}

		this.loading = true;

		const updatedSociety = this.societyForm.getValue();

		this.societyBll.updateSociety$(updatedSociety).subscribe({
			next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;
					return;
				}

				this.popupError = null;
				this.loadSocieties();
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
		if (!this.selectedSociety) return;

		this.loading = true;

		this.societyBll.deleteSociety$(this.selectedSociety.identifier).subscribe({
			next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;
					return;
				}

				this.popupError = null;
				this.loadSocieties();			
				this.toast.show(response.message, "success", 5000);	
				this.popupVisible = false;
			},
			error: err => {
				this.loading = false;
				this.popupError = err.error?.message || "Erreur inconnue";
			}
		});
	}

	toggleEnabled(society: SocietyModel) {
	this.loading = true;

	const updatedSociety: SocietyModel = {
		...society,
		isActive: !society.isActive 
	};

	this.societyBll.enabledSociety$(updatedSociety).subscribe({
			next: response => {
				this.loading = false;

				if (!response.isSuccess) {
					this.toast.show(response.message, "error", 5000);	
					return;
				}

				// Mise à jour locale
				this.societies = this.societies?.map(r =>
					r.identifier === society.identifier ? updatedSociety : r
				);

				this.toast.show(response.message, "success", 5000);	
			},
			error: err => {
				this.loading = false;
				console.error(err);
			}
		});
	}

}
