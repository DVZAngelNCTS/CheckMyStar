import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RoleFilterComponent } from './Filter/Role-filter.component';
import { RoleBllService } from '../../../60_Bll/BackOffice/Role-bll.service';
import { RoleModel } from '../../../20_Models/BackOffice/Role.model';
import { TableColumn } from '../../../70_Modules/Components/Table/Models/TableColumn.model'
import { TableComponent } from '../../Components/Table/Table.component'
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
	selector: 'app-role-page',
	standalone: true,
	imports: [CommonModule, RoleFilterComponent, FormsModule, ReactiveFormsModule, TableComponent,TranslationModule],
	templateUrl: './Role-page.component.html'
})
export class RolePageComponent {
	roles: RoleModel[] = [];

	columns = [
		{ field: 'identifier', header: 'RoleSection.Identifier', sortable: true, filterable: true, width: '25%' },
		{ field: 'name', header: 'RoleSection.Name', translate: true, sortable: true, filterable: true, width: '25%' },
		{ field: 'description', header: 'RoleSection.Description', sortable: true, filterable: true }
		] as TableColumn<RoleModel>[];

	constructor(private roleBll: RoleBllService) { 
	}

	ngOnInit() {
		this.loadRoles();
	}

	loadRoles() {
		this.roleBll.getRole$().subscribe({
			next: roles => this.roles = roles,
			error: err => console.error(err)
		});
	}

	onFilter(filter: { name?: string }) {
	this.roleBll.getRole$(filter.name).subscribe({
		next: roles => this.roles = roles,
		error: err => console.error(err)
	});
	}

	onUpdate(role: RoleModel) {

	}

	onDelete(role: RoleModel) {
		if (!confirm(`Supprimer le rôle ${role.name} ?`)) {
			return;
		}

		//this.roleBll.deleteRole$(role.identifier).subscribe({
		//	next: () => {
			// Mise à jour locale de la liste
		//	this.roles = this.roles.filter(r => r.identifier !== role.identifier);
		//	},
		//	error: err => console.error(err)
		//});
	}

	onAdd() {
		console.log("Ajouter un rôle");
		// Tu pourras ouvrir un modal, naviguer vers un formulaire, etc.
	}
}
