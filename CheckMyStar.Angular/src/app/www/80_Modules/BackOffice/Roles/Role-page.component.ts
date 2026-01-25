import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RoleFilterComponent } from './Filter/Role-filter.component';
import { RoleBllService } from '../../../60_Bll/BackOffice/Role-bll.service';
import { RoleModel } from '../../../20_Models/BackOffice/Role.model';
import { TableColumn } from '../../../80_Modules/Components/Table/Models/TableColumn.model'
import { TableComponent } from '../../Components/Table/Table.component'
import { TranslationModule } from '../../../10_Common/Translation.module';
import { PopupComponent } from '../../Components/Popup/Popup.component';
import { RoleFormComponent } from '../Roles/Form/Role-form.component'
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-role-page',
	standalone: true,
	imports: [CommonModule, RoleFilterComponent, FormsModule, ReactiveFormsModule, TableComponent,TranslationModule, PopupComponent, RoleFormComponent],
	templateUrl: './Role-page.component.html'
})
export class RolePageComponent {
	popupVisible = false; 
	popupMode: 'create' | 'edit' | 'delete' | null = null; 
	popupTitle = ''; 
	popupConfirmLabel = ''; 
	popupCancelLabel = ''; 
	selectedRole: RoleModel | null = null;
	@ViewChild(RoleFormComponent) roleForm!: RoleFormComponent;
	popupError: string | null = null;	
	roles?: RoleModel[] = [];
	loading = false;
	loadingSearch = false; 
	loadingReset = false;

	columns = [
		{ field: 'identifier', header: 'RoleSection.Identifier', sortable: true, filterable: true, width: '25%' },
		{ field: 'name', header: 'RoleSection.Name', translate: true, sortable: true, filterable: true, width: '25%' },
		{ field: 'description', header: 'RoleSection.Description', sortable: true, filterable: true }
		] as TableColumn<RoleModel>[];

	constructor(private roleBll: RoleBllService, private translate: TranslateService) { 
	}

	ngOnInit() {
		this.loadRoles();
	}

	loadRoles() {
		this.roleBll.getRoles$().subscribe({
			next: roles => this.roles = roles.roles,
			error: err => console.error(err)
		});
	}

	onFilter(filter: any) {

		if (filter.reset)
			this.loadingReset = true;
		else
			this.loadingSearch = true;

		this.roleBll.getRoles$(filter.name).subscribe({
			next: roles => {
				if (filter.reset)
						this.loadingReset = false;
					else
						this.loadingSearch = false;
				this.roles = roles.roles
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
		this.popupTitle = this.translate.instant('RoleSection.Create');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	openUpdate(role: RoleModel) {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedRole = role;
		this.popupMode = 'edit';
		this.popupTitle = this.translate.instant('RoleSection.Update');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	openDelete(role: RoleModel) {
		this.loading = false;
		this.loadingSearch = false;
		this.loadingReset = false;
		this.popupError = null;
		this.selectedRole = role;
		this.popupMode = 'delete';
		this.popupTitle = this.translate.instant('RoleSection.Delete');
		this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
		this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
		this.popupVisible = true;
	}

	onPopupConfirm() {

	if (this.popupMode === 'create') {
		if (this.roleForm.form.invalid) {
		this.roleForm.form.markAllAsTouched();
		return; // ❗ NE PAS FERMER LA POPUP
		}
		this.onCreateConfirmed();
		return;
	}

	if (this.popupMode === 'edit') {
		if (this.roleForm.form.invalid) {
		this.roleForm.form.markAllAsTouched();
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
	if (this.roleForm.form.invalid) {
		this.roleForm.form.markAllAsTouched();
		return;
	}

	this.loading = true;

	const newRole = this.roleForm.getValue();

	this.roleBll.addRole$(newRole).subscribe({
		next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;
					return; // ❗ ne pas fermer la popup
				}

				this.popupError = null;
				this.loadRoles();
				this.popupVisible = false;
			},
			error: err => {
				this.loading = false;
				this.popupError = err.error?.message || "Erreur inconnue";
			}
		});
	}

	onEditConfirmed() {
		if (this.roleForm.form.invalid) {
			this.roleForm.form.markAllAsTouched();
			return;
		}

		this.loading = true;

		const updatedRole = this.roleForm.getValue();

		this.roleBll.updateRole$(updatedRole).subscribe({
			next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;
					return;
				}

				this.popupError = null;
				this.loadRoles();
				this.popupVisible = false;
			},
			error: err => {
				this.loading = false;
				this.popupError = err.error?.message || "Erreur inconnue";
			}
		});
	}

	onDeleteConfirmed() {
		if (!this.selectedRole) return;

		this.loading = true;

		this.roleBll.deleteRole$(this.selectedRole.identifier).subscribe({
			next: response => {
				if (!response.isSuccess) {
					this.loading = false;
					this.popupError = response.message;
					return;
				}

				this.popupError = null;
				this.loadRoles();				
				this.popupVisible = false;
			},
			error: err => {
				this.loading = false;
				this.popupError = err.error?.message || "Erreur inconnue";
			}
		});
	}
}
