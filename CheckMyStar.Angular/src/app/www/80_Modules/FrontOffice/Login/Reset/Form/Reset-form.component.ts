import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RouterModule, ActivatedRoute } from '@angular/router';
import { TranslationModule } from '../../../../../10_Common/Translation.module';
import { AuthenticateService } from '../../../../../90_Services/Authenticate/Authenticate.service';
import { FieldComponent } from '../../../../Components/Field/Field.component';
import { Router } from '@angular/router';
import { MiniLoaderComponent } from '../../../../Components/Loader/Mini/Loader-mini.component';
import { LoaderManager } from '../../../../../90_Services/Loader/Loader-manager.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-reset-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule, TranslationModule, FieldComponent, MiniLoaderComponent],
  templateUrl: './Reset-form.component.html',
  styleUrls: ['./Reset-form.component.css']
})
export class ResetFormComponent implements OnInit {
  loading = false;
  loading$!: Observable<boolean>;
  form: FormGroup;
  errorMessage: string | null = null;
  passwordVisible = false;
  token!: string;

  constructor(private authenticateService: AuthenticateService, private fb: FormBuilder, private router: Router, private loaderManager: LoaderManager, private route: ActivatedRoute,) {    
    this.form = this.fb.group({
        password: ['', Validators.required],
        passwordverify: ['', Validators.required]
      });
  }

  ngOnInit() { 
    this.token = this.route.snapshot.queryParamMap.get('token')!;

    this.loading$ = this.loaderManager.register('login-button'); 
    this.loading$.subscribe(v => this.loading = v); 
  }
  
  generatePassword() {
    const pwd = this.authenticateService.generatePassword();

    // 4) On remplit le champ
    this.form.get('password')?.setValue(pwd);

    // Optionnel : rendre visible après génération
    this.passwordVisible = true;
  }

  resetPassword() {
    this.loaderManager.show('login-button');
    this.errorMessage = null;

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.loaderManager.hide('login-button');
      return;
    }

    const { password, passwordverify } = this.form.value;

    if (password !== passwordverify) {
      this.errorMessage = "Les mots de passe ne correspondent pas";
      this.loaderManager.hide('login-button');
      return;
    }    

    this.authenticateService.resetPassword$(this.token, password).subscribe({
      next: (result) => {
        this.loaderManager.hide('login-button'); 

        if (result.isSuccess) {
          localStorage.setItem('token', result.login.token);
          localStorage.setItem('refreshToken', result.login.refreshToken);
          localStorage.setItem('user', JSON.stringify(result.login.user));

          this.router.navigate(['/login']);
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