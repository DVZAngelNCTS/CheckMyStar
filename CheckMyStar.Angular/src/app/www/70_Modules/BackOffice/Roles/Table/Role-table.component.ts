import { Component, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from '../../../../10_Common/Translation.module';

@Component({
	selector: 'app-role-table',
	standalone: true,
	imports: [CommonModule, TranslationModule],
	templateUrl: './Role-table.component.html'
})
export class RoleTableComponent {
	constructor() { 
	}
    update(): void {

    }
    delete(): void {
        
  }
}