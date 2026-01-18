import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RoleFilterComponent } from './Filter/Role-filter.component';
import { RoleTableComponent } from './Table/Role-table.component';
import { RoleBllService } from '../../../60_Bll/BackOffice/Role-bll.service';
import { RoleModel } from '../../../20_Models/BackOffice/Role.model';

@Component({
	selector: 'app-role-page',
	standalone: true,
	imports: [CommonModule, RoleFilterComponent, FormsModule, ReactiveFormsModule, RoleTableComponent],
	templateUrl: './Role-page.component.html'
})
export class RolePageComponent {
	roles: RoleModel[] = [];

	constructor(private roleBll: RoleBllService) { 
	}

  	onFilter(filter: { name?: string }) {
    	this.roleBll.getRole$(filter.name).subscribe({
      		next: roles => this.roles = roles,
      		error: err => console.error(err)
   	 	});
  	}
}
