import { Component } from '@angular/core';
import { ForgotPasswordComponent } from './Form/Forgot-form.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';

@Component({
	selector: 'app-forgot-page',
	standalone: true,
	imports: [ForgotPasswordComponent, TranslationModule],
	templateUrl: './Forgot-page.component.html'
})
export class ForgotPageComponent {
}