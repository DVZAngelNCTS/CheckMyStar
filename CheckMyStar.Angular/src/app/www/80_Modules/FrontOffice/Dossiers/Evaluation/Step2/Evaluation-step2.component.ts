import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TranslationModule } from '../../../../../10_Common/Translation.module';
import { EvaluationFormData, CriterionEvaluation } from "../../../../../20_Models/FrontOffice/Evaluation.models";
import { TooltipDirective } from '../../../../Components/Tooltip/Tooltip.directive';
import { PopupComponent } from '../../../../Components/Popup/Popup.component';
import { getCriterionCategory, CriterionCategory } from '../../../../../10_Common/Utils/Criterion-category.util';

@Component({
  selector: 'app-eval-step2',
  standalone: true,
  imports: [CommonModule, FormsModule, TranslationModule, TooltipDirective, PopupComponent],
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

  showExplanationPopup = false;
  explanationTitle = '';
  explanationText = '';

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

  getViewedCount(): number {
    return this.criteriaEvaluations.filter(c => c.viewed).length;
  }

  allCriteriaViewed(): boolean {
    return this.criteriaEvaluations.length > 0 &&
      this.criteriaEvaluations.every(c => c.viewed);
  }

  checkAllViewedAndValidated(): void {
    this.criteriaEvaluations.forEach(c => {
      c.viewed = true;
      c.validated = true;
    });
  }

  onCriterionRowClick(criterion: CriterionEvaluation, event: MouseEvent): void {
    const target = event.target as HTMLElement | null;
    if (target?.closest('button, input, textarea, select, a, label')) {
      return;
    }

    if (!criterion.viewed && !criterion.validated) {
      criterion.viewed = true;
      criterion.validated = true;
      return;
    }

    if (criterion.viewed && criterion.validated) {
      criterion.viewed = true;
      criterion.validated = false;
      return;
    }

    criterion.viewed = false;
    criterion.validated = false;
  }

  getCriterionStateClass(criterion: CriterionEvaluation): string {
    if (criterion.viewed && criterion.validated) {
      return 'criterion-state-validated';
    }

    if (criterion.viewed && !criterion.validated) {
      return 'criterion-state-viewed-not-validated';
    }

    return 'criterion-state-initial';
  }

  getCriterionStatusLabelKey(criterion: CriterionEvaluation): string {
    if (criterion.viewed && criterion.validated) {
      return 'EvaluationSection.StatusViewedValidated';
    }

    if (criterion.viewed && !criterion.validated) {
      return 'EvaluationSection.StatusViewedNotValidated';
    }

    return 'EvaluationSection.StatusNotViewedNotValidated';
  }

  getCriterionStatusBadgeClass(criterion: CriterionEvaluation): string {
    if (criterion.viewed && criterion.validated) {
      return 'badge bg-success-subtle text-success-emphasis border border-success-subtle';
    }

    if (criterion.viewed && !criterion.validated) {
      return 'badge bg-danger-subtle text-danger-emphasis border border-danger-subtle';
    }

    return 'badge bg-light text-secondary border';
  }

  getTotalPoints(): number {
    return this.criteriaEvaluations
      .filter(c => c.validated)
      .reduce((sum, c) => sum + c.basePoints, 0);
  }

  getCategoryHeaderForRow(index: number, criteria: CriterionEvaluation[]): CriterionCategory | null {
    const current = criteria[index];
    const currentCat = current.category
      ? { category: current.category, section: current.section ?? '' }
      : getCriterionCategory(current.criterionId);
    if (!currentCat) return null;
    if (index === 0) return currentCat;
    const previous = criteria[index - 1];
    const previousCat = previous.category
      ? { category: previous.category, section: previous.section ?? '' }
      : getCriterionCategory(previous.criterionId);
    if (!previousCat || previousCat.category !== currentCat.category) return currentCat;
    return null;
  }

  onShowExplanation(criterion: CriterionEvaluation): void {
    this.explanationTitle = `${criterion.description}`;
    this.explanationText = criterion.explanation || 'Aucune explication disponible';
    this.showExplanationPopup = true;
  }

  closeExplanationPopup(): void {
    this.showExplanationPopup = false;
    this.explanationText = '';
  }
}
