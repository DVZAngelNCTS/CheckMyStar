import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RoleModel } from '../../../../20_Models/BackOffice/Role.model';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { DigitsOnlyDirective } from '../../../../10_Common/InputFilter/Digit-only'
import { RoleBllService } from '../../../../60_Bll/BackOffice/Role-bll.service';

@Component({
  selector: 'app-role-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FieldComponent, TranslationModule, DigitsOnlyDirective],
  templateUrl: './Role-form.component.html'
})
export class RoleFormComponent implements OnInit, OnChanges {
  @Input() role: RoleModel | null = null;
  @Input() readonlyIdentifier: boolean = false;

  form!: FormGroup;

  constructor(private fb: FormBuilder, private roleBll: RoleBllService) {}

  ngOnInit() {
    this.buildForm();

    // Toujours charger le prochain identifiant si on est en création
    if (!this.role) {
      this.loadIdentifiers();
    }

    // Toujours désactiver le champ si readonlyIdentifier = true
    if (this.readonlyIdentifier) {
      this.form.get('identifier')?.disable();
    }
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['role'] && changes['role'].currentValue) {

      if (!this.form) {
        this.buildForm();
      }

      this.form.patchValue({
        identifier: this.role?.identifier ?? '',
        name: this.role?.name ?? '',
        description: this.role?.description ?? '',
        isActive: this.role?.isActive ?? true
      });

      if (this.readonlyIdentifier) {
        this.form.get('identifier')?.disable();
      }
    }
  }

  /** Permet au parent de récupérer les valeurs */
  getValue(): RoleModel {
    return this.form.getRawValue() as RoleModel;
  }

  private buildForm() { 
    this.form = this.fb.group({
      identifier: [this.role?.identifier ?? '', Validators.required],
      name: [this.role?.name ?? '', Validators.required],
      description: [this.role?.description ?? '', Validators.required],
      isActive: [this.role?.isActive ?? true]
    });
  }

  loadIdentifiers() {
    this.roleBll.getNextIdentifier$().subscribe({
      next: role => this.form.get('identifier')?.patchValue(role.role?.identifier ?? 0),
      error: err => console.error(err)
    });
  }  
}
