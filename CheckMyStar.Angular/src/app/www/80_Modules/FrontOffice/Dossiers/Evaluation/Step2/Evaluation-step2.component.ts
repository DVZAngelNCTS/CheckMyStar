import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslationModule } from '../../../../../10_Common/Translation.module';
import { EvaluationFormData, CriterionEvaluation } from "../../../../../20_Models/FrontOffice/Evaluation.models";
import { TooltipDirective } from '../../../../Components/Tooltip/Tooltip.directive';

@Component({
  selector: 'app-eval-step2',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslationModule, TooltipDirective],
  templateUrl: './Evaluation-step2.component.html',
  styleUrl: './Evaluation-step2.component.css'
})
export class EvaluationStep2Component {
  @Input() form!: EvaluationFormData;
  @Input() criteriaEvaluations: CriterionEvaluation[] = [];
  @Input() lastSavedAt: string | null = null;
  @Output() goBack = new EventEmitter<void>();
  @Output() saveEval = new EventEmitter<void>();
  @Output() submitEval = new EventEmitter<void>();

  getStarArray(n: number): number[] {
    return Array.from({ length: n }, (_, i) => i);
  }

  getTypeBadgeClass(typeCode: string): string {
    switch (typeCode?.toUpperCase()) {
      case 'X':     return 'badge bg-primary';
      case 'X_ONC': return 'badge bg-danger';
      case 'O':     return 'badge bg-warning text-dark';
      case 'NA':    return 'badge bg-secondary';
      default:      return 'badge bg-light text-dark border';
    }
  }

  getValidatedCount(): number {
    return this.criteriaEvaluations.filter(c => c.validated).length;
  }

  getTotalPoints(): number {
    return this.criteriaEvaluations
      .filter(c => c.validated)
      .reduce((sum, c) => sum + c.basePoints, 0);
  }
}
