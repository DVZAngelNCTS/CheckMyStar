
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuComponent } from './Menu/Menu.component';

@Component({
	selector: 'app-home-page',
	standalone: true,
	imports: [CommonModule, MenuComponent],
	templateUrl: './Home-page.component.html'
})
export class HomePageComponent {
	sidebarOpen = false;

	onMenuToggle(open: boolean) {
		this.sidebarOpen = open;
	}
}
