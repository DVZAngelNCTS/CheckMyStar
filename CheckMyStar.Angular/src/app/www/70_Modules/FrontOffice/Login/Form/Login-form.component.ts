import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { AuthenticateService } from '../../../../80_Services/Authenticate.service';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule, TranslationModule, FieldComponent],
  templateUrl: './Login-form.component.html',
})
export class LoginFormComponent {
  form: FormGroup;
  errorMessage: string | null = null;

  constructor(private authenticateService: AuthenticateService, private fb: FormBuilder, private router: Router) {
      this.form = this.fb.group({
        login: ['', Validators.required],
        password: ['', Validators.required]
      });
  }

  login() {
    this.errorMessage = null;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const { login, password } = this.form.value;

    this.authenticateService.login(login, password).subscribe({
      next: (result) => {
        // Stocker le token
        localStorage.setItem('token', result.token);

        // Redirection
        this.router.navigate(['/home']);
      },
      error: (err) => {
        this.errorMessage = err.error?.message || "Erreur inconnue";
      }
    });
  }
}