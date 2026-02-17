import { Component } from '@angular/core';
import { PasswordFormComponent } from './Form/Password-form.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';

@Component({
	selector: 'app-password-page',
	standalone: true,
	imports: [PasswordFormComponent, TranslationModule],
	templateUrl: './Password-page.component.html'
})
export class PasswordPageComponent {
}