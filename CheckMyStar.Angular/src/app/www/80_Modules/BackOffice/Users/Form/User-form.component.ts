import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserModel } from '../../../../20_Models/Common/User.model';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { DigitsOnlyDirective } from '../../../../10_Common/InputFilter/Digit-only'

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
      lastName: [this.user?.lastName ?? '', Validators.required],
      firstName: [this.user?.firstName ?? '', Validators.required],
      isActive: [this.user?.isActive ?? true]
    });
  }

  ngOnChanges(changes: SimpleChanges) {
     if (changes['user'] && !changes['user'].firstChange) { 
      this.form.patchValue({ 
        identifier: this.user?.identifier ?? '', 
        lastName: this.user?.lastName ?? '', 
        firstName: this.user?.firstName ?? '',
        isActive: this.user?.isActive ?? true
      }); 
    }
  }  
}