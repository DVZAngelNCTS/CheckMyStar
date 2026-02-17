import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { AuthenticateService } from '../../../../90_Services/Authenticate/Authenticate.service';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { Router } from '@angular/router';
import { EnumRole } from '../../../../10_Common/Enumerations/EnumRole';
import { MiniLoaderComponent } from '../../../Components/Loader/Mini/Loader-mini.component';
import { LoaderManager } from '../../../../90_Services/Loader/Loader-manager.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-login-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule, TranslationModule, FieldComponent, MiniLoaderComponent],
  templateUrl: './Login-form.component.html',
})
export class LoginFormComponent implements OnInit {
  loading = false;
  loading$!: Observable<boolean>;
  form: FormGroup;
  errorMessage: string | null = null;
  passwordVisible = false;

  constructor(private authenticateService: AuthenticateService, private fb: FormBuilder, private router: Router, private loaderManager: LoaderManager) {    
    this.form = this.fb.group({
        login: ['', Validators.required],
        password: ['', Validators.required]
      });
  }

  ngOnInit() { 
    this.loading$ = this.loaderManager.register('login-button'); 
    this.loading$.subscribe(v => this.loading = v); 
  }

  login() {
    this.loaderManager.show('login-button');
    this.errorMessage = null;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.loaderManager.hide('login-button');
      return;
    }

    const { login, password } = this.form.value;

    this.authenticateService.login$(login, password).subscribe({
    next: (result) => {   
      this.loaderManager.hide('login-button');     

      if (result.isSuccess && result.isValid) {
        const user = result.login.user;
        
        localStorage.setItem('token', result.login.token);
        localStorage.setItem('refreshToken', result.login.refreshToken);
        localStorage.setItem('user', JSON.stringify(user));

        // 👉 Première connexion : redirection vers changement de mot de passe
        if (user.isFirstConnection) {
          this.router.navigate(['/password']);
          return;
        }

        // 👉 Sinon : redirection normale
        if (user.role === EnumRole.Administrator) {
          this.router.navigate(['/backhome']);
        } else {
          this.router.navigate(['/fronthome']);
        }
      }
      else
      {
        this.loaderManager.hide('login-button')
        this.errorMessage = result.message;        
      }
    },
    error: (err) => {      
        this.loaderManager.hide('login-button')
        this.errorMessage = err.error?.message || "Erreur inconnue";
      }
    });
  }

  togglePasswordVisibility() {
    this.passwordVisible = !this.passwordVisible;
  }
}