import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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

@Component({
	selector: 'app-user-page',
	standalone: true,
	imports: [CommonModule, UserFilterComponent, FormsModule, ReactiveFormsModule, TableComponent,TranslationModule, PopupComponent, UserFormComponent],
	templateUrl: './User-page.component.html'
})
export class UserPageComponent {
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

	columns = [
		{ icon: 'bi bi-list-ol', field: 'identifier', header: 'UserSection.Identifier', sortable: true, filterable: true, width: '10%' },
		//{ icon: 'bi bi-gender-ambiguous', field: 'civility', header: 'UserSection.Civility', translate: true, sortable: true, filterable: true, width: '10%', 
		//	pipe: (value) => this.translate.instant( value === EnumCivility.Mister ? 'UserSection.Mister' : value === EnumCivility.Madam ? 'UserSection.Madam' : '') },
		{ icon: 'bi bi-person', field: 'lastName', header: 'UserSection.LastName', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-person', field: 'firstName', header: 'UserSection.FirstName', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-building', field: 'society', header: 'UserSection.Society', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-envelope-at', field: 'email', header: 'UserSection.Email', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-telephone', field: 'phone', header: 'UserSection.Phone', translate: true, sortable: true, filterable: true, width: '10%' },
		{ icon: 'bi bi-envelope', field: 'address', header: 'UserSection.Address', translate: true, sortable: true, filterable: true, width: '10%',
		 	pipe: (a) => { if (!a) return ''; const line1 = [a.number, a.addressLine].filter(Boolean).join(' '); const line2 = [a.zipCode, a.city].filter(Boolean).join(' '); const country = a.country?.name ?? ''; return [line1, line2, country].filter(Boolean).join(', '); }},
		{ icon: 'bi bi-shield', field: 'role', header: 'RoleSection.Role', translate: true, sortable: true, filterable: true, width: '10%',
			pipe: (value) => this.translate.instant( value === EnumRole.Administrator ? 'RoleSection.Administrator' : value === EnumRole.User ? 'RoleSection.User' : value === EnumRole.Inspector ? 'RoleSection.Inspector' : '') },
		{ icon: 'bi bi-shield-check', field: 'isActive', header: 'UserSection.Active', sortable: true, filterable: false }
		] as TableColumn<UserModel>[];

	constructor(private userBll: UserBllService, private translate: TranslateService, private toast: ToastService) { 
	}

	ngOnInit() {
		this.loadUsers();
	}

	loadUsers() {
		this.userBll.getUsers$().subscribe({
			next: users => this.users = users.users,
			error: err => console.error(err)
		});
	}

	onFilter(filter: any) {

		if (filter.reset)
			this.loadingReset = true;
		else
			this.loadingSearch = true;

		this.userBll.getUsers$(filter.name, filter.firstName, filter.society, filter.email, filter.phone, filter.address, filter.role).subscribe({
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

	const newUser = this.getValue();
		
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

		const updatedUser = this.getValue();

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

	getValue(): UserModel {
  		return this.userForm.form.getRawValue() as UserModel;
	}

	toggleEnabled(user: UserModel) {
		this.loading = true;

		const updatedUser: UserModel = {
			...user,
			isActive: !user.isActive 
		};

		this.userBll.updateUser$(updatedUser).subscribe({
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
					console.error(err);
				}
			});
	}
}
