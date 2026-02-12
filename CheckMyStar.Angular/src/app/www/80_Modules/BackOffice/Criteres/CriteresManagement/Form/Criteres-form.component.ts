import { Component, Input, OnInit, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FieldComponent } from '../../../../Components/Field/Field.component';
import { TranslationModule } from '../../../../../10_Common/Translation.module';
import { StarCriterionDetail } from '../../../../../20_Models/BackOffice/Criteres.model';

@Component({
  selector: 'app-criteres-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FieldComponent, TranslationModule],
  templateUrl: './Criteres-form.component.html'
})
export class CriteresFormComponent implements OnInit, OnChanges {
  @Input() criterion: StarCriterionDetail | null = null;
  @Input() readonlyIdentifier: boolean = true;

  form!: FormGroup;

  typeCodes = [
    { value: 'X', label: 'X - Obligatoire' },
    { value: 'O', label: 'O - À la carte' },
    { value: 'X ONC', label: 'X ONC - Obligatoire non compensable' },
    { value: 'NA', label: 'NA - Non applicable' }
  ];

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.buildForm();
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['criterion'] && changes['criterion'].currentValue && this.form) {
      this.form.patchValue({
        criterionId: this.criterion?.criterionId ?? 0,
        description: this.criterion?.description ?? '',
        basePoints: this.criterion?.basePoints ?? 0,
        typeCode: this.criterion?.typeCode ?? 'X'
      });
      
      if (this.criterion !== null) {
        this.form.controls['criterionId'].disable();
      } else {
        this.form.controls['criterionId'].enable();
      }
    }
  }

  getValue(): StarCriterionDetail {
    return this.form.getRawValue() as StarCriterionDetail;
  }

  private buildForm() {
    const isEditMode = this.criterion !== null;

    this.form = this.fb.group({
      criterionId: [{ 
        value: this.criterion?.criterionId ?? 0, 
        disabled: isEditMode
      }],
      description: [this.criterion?.description ?? '', Validators.required],
      basePoints: [
        this.criterion?.basePoints ?? 1,
        [Validators.required, Validators.min(1), Validators.max(10)]
      ],
      typeCode: [this.criterion?.typeCode ?? 'X', Validators.required]
    });
  }
}