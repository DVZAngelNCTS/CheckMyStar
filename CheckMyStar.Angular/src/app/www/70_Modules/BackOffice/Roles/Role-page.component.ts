import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RoleFilterComponent } from './Filter/Role-filter.component';
import { RoleTableComponent } from './Table/Role-table.component';

@Component({
	selector: 'app-role-page',
	standalone: true,
	imports: [CommonModule, RoleFilterComponent, FormsModule, ReactiveFormsModule, RoleTableComponent],
	templateUrl: './Role-page.component.html'
})
export class RolePageComponent {
	constructor() { 
	}
}
