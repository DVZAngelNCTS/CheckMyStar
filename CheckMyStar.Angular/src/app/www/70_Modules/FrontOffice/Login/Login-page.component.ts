import { Component } from '@angular/core';
import { LoginFormComponent } from './Form/Login-form.component';
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
	selector: 'app-login-page',
	standalone: true,
	imports: [LoginFormComponent, TranslationModule],
	templateUrl: './Login-page.component.html'
})
export class LoginPageComponent {
}