import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RoleModel } from '../../../../20_Models/BackOffice/Role.model';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';

@Component({
  selector: 'app-role-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FieldComponent, TranslationModule],
  templateUrl: './Role-form.component.html'
})
export class RoleFormComponent implements OnInit {

  @Input() role: RoleModel | null = null;

  form!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.form = this.fb.group({
      identifier: [this.role?.identifier ?? '', Validators.required],
      name: [this.role?.name ?? '', Validators.required],
      description: [this.role?.description ?? '', Validators.required]
    });
  }

  /** Permet au parent de récupérer les valeurs */
  getValue(): RoleModel {
    return this.form.value as RoleModel;
  }
}
