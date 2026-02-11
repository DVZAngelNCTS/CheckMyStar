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
    { value: 'X', label: 'Obligatoire' },
    { value: 'O', label: 'À la carte' },
    { value: 'X ONC', label: 'Obligatoire non compensable' },
    { value: 'NA', label: 'Non applicable' }
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
    }
  }

  getValue(): StarCriterionDetail {
    return this.form.value as StarCriterionDetail;
  }

  private buildForm() { 
    this.form = this.fb.group({
      criterionId: [this.criterion?.criterionId ?? 0],
      description: [this.criterion?.description ?? '', Validators.required],
      basePoints: [this.criterion?.basePoints ?? 0, [Validators.required, Validators.min(0)]],
      typeCode: [this.criterion?.typeCode ?? 'X', Validators.required]
    });
  }
}