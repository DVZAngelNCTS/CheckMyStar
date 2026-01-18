import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { RoleModel } from '../../../../20_Models/BackOffice/Role.model';

@Component({
	selector: 'app-role-table',
	standalone: true,
	imports: [CommonModule, TranslationModule],
	templateUrl: './Role-table.component.html'
})
export class RoleTableComponent {
	roles = input<RoleModel[]>([]);

	constructor() { 
	}
    update(role: RoleModel): void {

    }
    delete(identifier: number): void {
        
  }
}