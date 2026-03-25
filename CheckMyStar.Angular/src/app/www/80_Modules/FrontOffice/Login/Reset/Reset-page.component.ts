import { Component } from '@angular/core';
import { ResetFormComponent } from './Form/Reset-form.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';

@Component({
	selector: 'app-reset-page',
	standalone: true,
	imports: [ResetFormComponent, TranslationModule],
	templateUrl: './Reset-page.component.html'
})
export class ResetPageComponent {
}