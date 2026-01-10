import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
	selector: 'app-role-page',
	standalone: true,
	imports: [CommonModule],
	templateUrl: './Role-page.component.html'
})
export class RolePageComponent {
	constructor() { 
		console.log("ROLE PAGE LOADED"); 
	}
}
