
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './Menu/Menu.component';

@Component({
	selector: 'app-front-home-page',
	standalone: true,
	imports: [CommonModule, MenuComponent],
	templateUrl: './Home-page.component.html'
})
export class FrontHomePageComponent {
	sidebarOpen = false;

	onMenuToggle(open: boolean) {
		this.sidebarOpen = open;
	}
}
