import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserFilterComponent } from './Filter/User-filter.component';
import { UserBllService } from '../../../60_Bll/BackOffice/User-bll.service';
import { UserModel } from '../../../20_Models/Common/User.model';
import { TableColumn } from '../../../80_Modules/Components/Table/Models/TableColumn.model'
import { TableComponent } from '../../Components/Table/Table.component'
import { TranslationModule } from '../../../10_Common/Translation.module';
import { PopupComponent } from '../../Components/Popup/Popup.component';
import { UserFormComponent } from '../Users/Form/User-form.component'
import { TranslateService } from '@ngx-translate/core';
import { EnumCivility } from '../../../10_Common/Enumerations/EnumCivility';
import { EnumRole } from '../../../10_Common/Enumerations/EnumRole';
import { ToastService } from '../../../90_Services/Toast/Toast.service';
import { SocietyBllService } from '../../../60_Bll/BackOffice/Society-bll.service';
import { AddressBllService } from '../../../60_Bll/BackOffice/Address-bll.service';
import { CountryBllService } from '../../../60_Bll/BackOffice/Country-bll.service';
import { SocietyModel } from '../../../20_Models/BackOffice/Society.model';
import { SocietyFormComponent } from '../../BackOffice/Societies/Form/Society-form.component';

@Component({
	selector: 'app-user-page',
	standalone: true,
	imports: [CommonModule, UserFilterComponent, FormsModule, ReactiveFormsModule, TableComponent,TranslationModule, PopupComponent, UserFormComponent, SocietyFormComponent],
	templateUrl: './User-page.component.html'
})
export class UserPageComponent {
	popupSocietyVisible = false;
	popupSocietyTitle = '';
	popupSocietyConfirmLabel = '';
	popupSocietyCancelLabel = '';
	popupSocietyError: string | null = null;
	@ViewChild(SocietyFormComponent) societyForm!: SocietyFormComponent;

	popupVisible = false; 
	popupMode: 'create' | 'edit' | 'delete' | null = null;
	popupTitle = ''; 
	popupConfirmLabel = ''; 
	popupCancelLabel = ''; 
	selectedUser: UserModel | null = null;
	@ViewChild(UserFormComponent) userForm!: UserFormComponent;
	popupError: string | null = null;	
	users?: UserModel[] = [];
	loading = false;
	loadingSearch = false; 
	loadingReset = false;
	createdSocietyId = 0;
	
	societies: SocietyModel[] = [];

	columns = [
		{ icon: 'bi bi-list-ol', field: 'identifier', header: 'UserSection.Identifier', sortable: true, filterable: true, width: '10%' },
		//{ icon: 'bi bi-gender-ambiguous', field: 'civility', header: 'UserSection.Civility', translate: true, sortable: true, filterable: true, width: '10%', 
		//	pipe: (value) => this.translate.instant( value === EnumCivility.Mister ? 'UserSection.Mister' : value === EnumCivility.Madam ? 'UserSection.Madam' : '') },
		{ icon: 'bi bi-person', field: 'lastName', header: 'UserSection.LastName', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-person', field: 'firstName', header: 'UserSection.FirstName', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-building', field: 'societyIdentifier', header: 'UserSection.Society', translate: true, sortable: true, filterable: true, width: '10%',
			pipe: (id) => { if (!id) return '';	const society = this.societies.find(s => s.identifier === id);return society ? society.name : '';}},
		{ icon: 'bi bi-envelope-at', field: 'email', header: 'UserSection.Email', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-telephone', field: 'phone', header: 'UserSection.Phone', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-envelope', field: 'address', header: 'UserSection.Address', translate: true, sortable: true, filterable: true, width: '10%',
		 	pipe: (a) => { if (!a) return ''; const line1 = [a.number, a.addressLine].filter(Boolean).join(' '); const line2 = [a.zipCode, a.city].filter(Boolean).join(' '); const country = a.country?.name ?? ''; return [line1, line2, country].filter(Boolean).join(', '); }},
		{ icon: 'bi bi-shield', field: 'role', header: 'RoleSection.Role', translate: true, sortable: true, filterable: true, width: '10%',
			pipe: (value) => this.translate.instant( value === EnumRole.Administrator ? 'RoleSection.Administrator' : value === EnumRole.User ? 'RoleSection.User' : value === EnumRole.Inspector ? 'RoleSection.Inspector' : '') },
		{ icon: 'bi bi-shield-check', field: 'isActive', header: 'UserSection.Active', sortable: true, filterable: false }
		] as TableColumn<UserModel>[];

	constructor(private fb: FormBuilder, private userBll: UserBllService, private translate: TranslateService, private toast: ToastService, private societyBll: SocietyBllService, private addressBll: AddressBllService, private countryBll: CountryBllService) { 
	}

	ngOnInit() {
		this.loadUsers();
		this.loadSocieties();
	}

	loadUsers() {
		this.userBll.getUsers$().subscribe({
			next: users => this.users = users.users,
			error: err => console.error(err)
		});
	}

	loadSocieties() {
		this.societyBll.getSocieties$().subscribe({
			next: (response) => {
				this.societies = response.societies || [];
			},
			error: (err) => console.error('Erreur chargement sociétés', err)
		});
	}

	onFilter(filter: any) {

		if (filter.reset)
			this.loadingReset = true;
		else
			this.loadingSearch = true;

		this.userBll.getUsers$(filter.lastName, filter.firstName, filter.societyIdentifier, filter.email, filter.phone, filter.address, filter.role).subscribe({
			next: users => {
				if (filter.reset)
						this.loadingReset = false;
					else
						this.loadingSearch = false;
				this.users = users.users
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
		this.createdSocietyId = 0;
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.popupMode = 'create';
		this.popupTitle = this.translate.instant('UserSection.Create');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	openUpdate(user: UserModel) {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedUser = user;
		this.popupMode = 'edit';
		this.popupTitle = this.translate.instant('UserSection.Update');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	openDelete(user: UserModel) {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedUser = user;
		this.popupMode = 'delete';
		this.popupTitle = this.translate.instant('UserSection.Delete');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	onPopupConfirm() {
		if (this.popupMode === 'create') {
			if (this.userForm.form.invalid) {
			this.userForm.form.markAllAsTouched();
			return; // ❗ NE PAS FERMER LA POPUP
			}
			this.onCreateConfirmed();
			return;
		}

		if (this.popupMode === 'edit') {
			if (this.userForm.form.invalid) {
			this.userForm.form.markAllAsTouched();
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
		if (this.userForm.form.invalid) {
			this.userForm.form.markAllAsTouched();
			return;
		}

		this.loading = true;

		const newUser = this.userForm.getValue();

		newUser.role = Number(newUser.role) as EnumRole;
		newUser.civility = Number(newUser.civility) as EnumCivility;

		this.userBll.addUser$(newUser).subscribe({
			next: response => {
					if (!response.isSuccess) {
						this.loading = false;
						this.popupError = response.message;
						return; // ❗ ne pas fermer la popup
					}

					this.popupError = null;
					this.loadUsers();
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
		if (this.userForm.form.invalid) {
			this.userForm.form.markAllAsTouched();
			return;
		}

		this.loading = true;

		const updatedUser = this.userForm.getValue();

		updatedUser.role = Number(updatedUser.role) as EnumRole;
		updatedUser.civility = Number(updatedUser.civility) as EnumCivility;

		this.userBll.updateUser$(updatedUser).subscribe({
			next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;
					return;
				}

				this.popupError = null;
				this.loadUsers();
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
		if (!this.selectedUser) return;

		this.loading = true;

		this.userBll.deleteUser$(this.selectedUser.identifier).subscribe({
			next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;
					return;
				}

				this.popupError = null;
				this.loadUsers();		
				this.toast.show(response.message, "success", 5000);		
				this.popupVisible = false;
			},
			error: err => {
				this.loading = false;
				this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
			}
		});
	}

	toggleEnabled(user: UserModel) {
		this.loading = true;

		const updatedUser: UserModel = {
			...user,
			isActive: !user.isActive 
		};

		this.userBll.enabledUser$(updatedUser).subscribe({
			next: response => {
				this.loading = false;

				if (!response.isSuccess) {
					this.toast.show(response.message, "error", 5000);	
					return;
				}

				// Mise à jour locale
				this.users = this.users?.map(r =>
					r.identifier === user.identifier ? updatedUser : r
				);

				this.toast.show(response.message, "success", 5000);	
			},
			error: err => {
				this.loading = false;
				
				this.toast.show(err, "error", 5000);	
			}
		});
	}

	openCreateSociety() {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupSocietyError = null;
		this.popupMode = 'create';
		this.popupSocietyTitle = this.translate.instant('SocietySection.Create');
		this.popupSocietyConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupSocietyCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupSocietyVisible = true;
		this.popupVisible = false;
	}

	onCreateSocietyConfirmed() {
		if (this.societyForm.form.invalid) {
			this.societyForm.form.markAllAsTouched();
			return;
		}

		this.loading = true;	

		const newSociety = this.societyForm.getValue();

		this.createdSocietyId = newSociety.identifier;

		this.societyBll.addSociety$(newSociety).subscribe({
			next: response => {
					if (!response.isSuccess) {
						this.loading = false;
						this.popupSocietyError = response.message;
						return; // ❗ ne pas fermer la popup
					}

					this.societyBll.getSociety$(newSociety.identifier).subscribe({
						next: response => {
							if (!response.isSuccess) {
								this.loading = false;
								this.popupSocietyError = response.message;
								return; // ❗ ne pas fermer la popup								
							}

							this.loadSocieties();
							
							this.userForm.loadSocieties();

							this.userForm.form.get('societyIdentifier')?.patchValue(newSociety.identifier);
						},
						error: err => {
							this.loading = false;
							this.popupSocietyError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
						}
					});

					this.popupSocietyError = null;
					this.toast.show(response.message, "success", 5000);
					this.popupSocietyVisible = false;
					this.popupVisible = true;
				},
				error: err => {
					this.loading = false;
					this.popupSocietyError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
				}
		});
	}

	onCreatePopupSocietyCancel() {
		this.popupSocietyVisible = false;
		this.popupVisible = true;
	}
}