import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { EvaluationResultModel } from '../../../../20_Models/BackOffice/EvaluationResult.model';
import { AssessmentCriterionModel } from '../../../../20_Models/BackOffice/AssessmentCriterion.model';
import { AssessmentModel } from '../../../../20_Models/BackOffice/Assessment.model';
import { AssessmentBllService } from '../../../../60_Bll/BackOffice/Assessment-bll.service';
import { AssessmentCriteriaResponse } from '../../../../50_Responses/BackOffice/AssessmentCriteria.response';
import { AssessmentResponse } from '../../../../50_Responses/BackOffice/Assessment.response';
import { PopupComponent } from '../../../Components/Popup/Popup.component';
import { getCriterionCategory, CriterionCategory } from '../../../../10_Common/Utils/Criterion-category.util';

@Component({
  selector: 'app-evaluation-view-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, RouterModule, PopupComponent],
  templateUrl: './EvaluationView-page.component.html',
  styleUrl: './EvaluationView-page.component.css'
})
export class EvaluationViewPageComponent implements OnInit {
  result: EvaluationResultModel | null = null;
  folderId: number | null = null;

  criteria: AssessmentCriterionModel[] = [];
  loadingCriteria = false;

  assessment: AssessmentModel | null = null;
  loadingAssessment = false;

  // Explanation popup state
  showExplanationPopup = false;
  explanationTitle = '';
  explanationText = '';
  get mandatoryOk(): boolean {
    return !!this.result && this.result.mandatoryPointsEarned >= this.result.mandatoryThreshold;
  }

  get optionalOk(): boolean {
    return !!this.result && this.result.optionalPointsEarned >= this.result.optionalRequired;
  }

  get oncOk(): boolean {
    return !!this.result && this.result.onceFailedCount === 0;
  }

  get validatedCount(): number {
    return this.criteria.filter(c => c.isValidated).length;
  }

  get totalPoints(): number {
    return this.criteria.filter(c => c.isValidated).reduce((s, c) => s + c.points, 0);
  }

  get activeConstraints(): string[] {
    if (!this.assessment) return [];
    const list: string[] = [];
    if (this.assessment.isWhiteZone)           list.push('EvaluationSection.ZoneBlanche');
    if (this.assessment.isDromTom)             list.push('EvaluationSection.DromTom');
    if (this.assessment.isHighMountain)        list.push('EvaluationSection.HauteMontagne');
    if (this.assessment.isBuildingClassified)  list.push('EvaluationSection.BatimentClasse');
    if (this.assessment.isStudioNoLivingRoom)  list.push('EvaluationSection.StudioSansSejouur');
    if (this.assessment.isParkingImpossible)   list.push('EvaluationSection.StationnementImpossible');
    return list;
  }

  constructor(private router: Router, private route: ActivatedRoute, private assessmentBll: AssessmentBllService) {
    // getCurrentNavigation() doit être appelé dans le constructor
    const nav = this.router.getCurrentNavigation();
    const state = nav?.extras?.state as { result?: EvaluationResultModel; folderId?: number } | undefined;
    if (state?.result) {
      this.result = state.result;
    }
    if (state?.folderId) {
      this.folderId = state.folderId;
    }
  }

  ngOnInit(): void {
    // Fallback folderId depuis la route si non fourni via le state
    if (!this.folderId) {
      const id = this.route.snapshot.paramMap.get('id');
      this.folderId = id ? +id : null;
    }

    if (!this.result) {
      // state perdu (ex : rafraîchissement) → retour au dossier
      if (this.folderId) {
        this.router.navigate(['/fronthome/dossiers', this.folderId]);
      } else {
        this.router.navigate(['/fronthome/dossiers']);
      }
      return;
    }

    this.loadCriteria();
    this.loadAssessment();
  }

  private loadCriteria(): void {
    if (!this.result?.assessmentIdentifier) return;
    this.loadingCriteria = true;
    this.assessmentBll.getAssessmentCriteria$(this.result.assessmentIdentifier).subscribe({
      next: (response: AssessmentCriteriaResponse) => {
        this.criteria = (response.assessmentCriteria ?? [])
          .sort((a, b) => a.criterionId - b.criterionId)
          .map(c => {
            const cat = getCriterionCategory(c.criterionId);
            return { ...c, category: cat?.category, section: cat?.section };
          });
        this.loadingCriteria = false;
      },
      error: () => {
        this.criteria = [];
        this.loadingCriteria = false;
      }
    });
  }

  private loadAssessment(): void {
    if (!this.result?.assessmentIdentifier) return;
    this.loadingAssessment = true;
    this.assessmentBll.getAssessment$(this.result.assessmentIdentifier).subscribe({
      next: (response: AssessmentResponse) => {
        this.assessment = response.assessment ?? null;
        this.loadingAssessment = false;
      },
      error: () => {
        this.assessment = null;
        this.loadingAssessment = false;
      }
    });
  }

  getTypeBadgeClass(status: string): string {
    switch (status?.toUpperCase()) {
      case 'X_ONC': return 'bg-danger';
      case 'X':     return 'bg-warning text-dark';
      case 'O':     return 'bg-info text-dark';
      default:      return 'bg-secondary';
    }
  }

  onBack(): void {
    if (this.folderId) {
      this.router.navigate(['/fronthome/dossiers', this.folderId]);
    } else {
      this.router.navigate(['/fronthome/dossiers']);
    }
  }

  getCategoryHeaderForRow(index: number, criteria: AssessmentCriterionModel[]): CriterionCategory | null {
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

  onShowExplanation(criterion: AssessmentCriterionModel): void {
    this.explanationTitle = `${criterion.criterionDescription}`;
    this.explanationText = criterion.explanation || 'Aucune explication disponible';
    this.showExplanationPopup = true;
  }

  closeExplanationPopup(): void {
    this.showExplanationPopup = false;
    this.explanationText = '';
  }
}
