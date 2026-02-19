
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MenuComponent } from '../../Components/Menu/Menu.component';
import { BreadcrumbComponent } from '../../Components/Breadcrumb/Breadcrumb.component';
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
	selector: 'app-front-home-page',
	standalone: true,
	imports: [CommonModule, RouterModule, MenuComponent, BreadcrumbComponent, TranslationModule],
	templateUrl: './Home-page.component.html'
})
export class FrontHomePageComponent {
	sidebarOpen = false;

	onMenuToggle(open: boolean) {
		this.sidebarOpen = open;
	}
}
