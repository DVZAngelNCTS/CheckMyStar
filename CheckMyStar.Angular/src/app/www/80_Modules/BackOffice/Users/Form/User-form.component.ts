import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserModel } from '../../../../20_Models/Common/User.model';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { DigitsOnlyDirective } from '../../../../10_Common/InputFilter/Digit-only'
import { EnumCivility } from '../../../../10_Common/Enumerations/EnumCivility';
import { EnumRole } from '../../../../10_Common/Enumerations/EnumRole';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FieldComponent, TranslationModule, DigitsOnlyDirective],
  templateUrl: './User-form.component.html'
})
export class UserFormComponent implements OnInit, OnChanges {
  @Input() user: UserModel | null = null;
  @Input() readonlyIdentifier: boolean = false;

  form!: FormGroup;

  EnumCivility = EnumCivility;
  EnumRole = EnumRole;

  passwordVisible = false;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.buildForm();
  }

  /** Permet au parent de récupérer les valeurs */
  getValue(): UserModel {
    return this.form.value as UserModel;
  } 

  private buildForm() { 
    this.form = this.fb.group({
      identifier: [this.user?.identifier ?? '', Validators.required],
      civility: [this.user?.civility ?? EnumCivility.Mister, Validators.required],      
      lastName: [this.user?.lastName ?? '', Validators.required],
      firstName: [this.user?.firstName ?? '', Validators.required],
      society: [this.user?.society ?? ''],
      email: [this.user?.email ?? '', [Validators.required, Validators.email]],
      phone: [this.user?.phone ?? ''],
      role: [this.user?.role ?? EnumRole.User, Validators.required], 
      password: [this.user?.password ?? '', Validators.required],
      address: this.fb.group({ 
        number: [this.user?.address?.number ?? ''], 
        addressLine: [this.user?.address?.addressLine ?? ''], 
        city: [this.user?.address?.city ?? ''], 
        zipCode: [this.user?.address?.zipCode ?? ''], 
        region: [this.user?.address?.region ?? ''], 
        country: [this.user?.address?.country ?? ''] 
      }),
      isActive: [this.user?.isActive ?? true]          
    });
  }

  ngOnChanges(changes: SimpleChanges) {
     if (changes['user'] && !changes['user'].firstChange) { 
      this.form.patchValue({ 
        identifier: this.user?.identifier ?? '', 
        civility: this.user?.civility ?? EnumCivility.Mister,
        lastName: this.user?.lastName ?? '', 
        firstName: this.user?.firstName ?? '',
        society: this.user?.society ?? '',
        email: this.user?.email ?? '',
        phone: this.user?.phone ?? '',
        role: this.user?.role ?? EnumRole.User,
        password: this.user?.password ?? '',
        address: { 
          number: this.user?.address?.number ?? '', 
          addressLine: this.user?.address?.addressLine ?? '', 
          city: this.user?.address?.city ?? '', 
          zipCode: this.user?.address?.zipCode ?? '', 
          region: this.user?.address?.region ?? '', 
          country: this.user?.address?.country ?? ''
        },
        isActive: this.user?.isActive ?? true
      }); 
    }
  }  

  togglePasswordVisibility() {
    this.passwordVisible = !this.passwordVisible;
  }
}