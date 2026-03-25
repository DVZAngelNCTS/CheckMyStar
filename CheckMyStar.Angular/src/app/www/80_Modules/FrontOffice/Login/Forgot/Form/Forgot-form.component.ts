import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TranslationModule } from '../../../../../10_Common/Translation.module';
import { FieldComponent } from '../../../../Components/Field/Field.component';
import { LoginBllService } from '../../../../../60_Bll/FrontOffice/Login-bll.service';
import { ToastService } from '../../../../../90_Services/Toast/Toast.service';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  templateUrl: './Forgot-form.component.html',
  imports: [ReactiveFormsModule, CommonModule, TranslationModule, FieldComponent]
})
export class ForgotPasswordComponent {
    form: FormGroup;

    constructor(private fb: FormBuilder, private loginBll: LoginBllService, private toast: ToastService) {
        this.form = this.fb.group({
        email: ['', [Validators.required, this.emailValidator]]
        });
    }

    submit() {
        if (this.form.valid) {
            const email = this.form.value.email;

            this.loginBll.forgotPassword$(email).subscribe({
                next: (response) => {
                    this.toast.show(response.message, "success", 5000);
                },
                error: (err) => {
                    this.toast.show(err.message, "error", 5000);
                }
            });
        }
    }

    private emailValidator(control: any) {
        const value = control.value || '';

        // Regex email robuste (RFC 5322 simplifiée)
        const emailRegex =
        /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;

        return emailRegex.test(value) ? null : { invalidEmail: true };
    }
}