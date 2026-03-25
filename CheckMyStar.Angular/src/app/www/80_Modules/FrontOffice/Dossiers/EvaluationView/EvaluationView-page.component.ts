import { Component, OnDestroy, OnInit } from '@angular/core';
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

type CriteriaSortColumn = 'criterionId' | 'criterionDescription' | 'status' | 'points' | 'isValidated' | 'explanation' | 'comment';
type SortDirection = 'asc' | 'desc';

@Component({
  selector: 'app-evaluation-view-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, RouterModule, PopupComponent],
  templateUrl: './EvaluationView-page.component.html',
  styleUrl: './EvaluationView-page.component.css'
})
export class EvaluationViewPageComponent implements OnInit, OnDestroy {
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
  highlightedCriterionId: number | null = null;
  showInvalidCriteriaByCategory = false;
  criteriaSearchTerm = '';
  criteriaSortColumn: CriteriaSortColumn = 'criterionId';
  criteriaSortDirection: SortDirection = 'asc';
  private highlightTimeout: ReturnType<typeof setTimeout> | null = null;

  get invalidCriteriaByCategory(): { section: string; category: string; items: AssessmentCriterionModel[] }[] {
    const groups: { section: string; category: string; items: AssessmentCriterionModel[] }[] = [];
    const groupMap = new Map<string, { section: string; category: string; items: AssessmentCriterionModel[] }>();

    for (const criterion of this.criteria) {
      if (criterion.isValidated) continue;

      const categoryInfo = criterion.category
        ? { category: criterion.category, section: criterion.section ?? '' }
        : getCriterionCategory(criterion.criterionId);

      const section = categoryInfo?.section || 'Section non categorisee';
      const category = categoryInfo?.category || 'Categorie non categorisee';
      const key = `${section}|||${category}`;

      if (!groupMap.has(key)) {
        const group = { section, category, items: [] as AssessmentCriterionModel[] };
        groupMap.set(key, group);
        groups.push(group);
      }

      groupMap.get(key)!.items.push(criterion);
    }

    return groups;
  }
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

  get invalidCriteriaCount(): number {
    return this.criteria.filter(c => !c.isValidated).length;
  }

  get filteredCriteria(): AssessmentCriterionModel[] {
    const term = this.criteriaSearchTerm.trim().toLowerCase();
    if (!term) return this.criteria;

    return this.criteria.filter(c => {
      const searchText = [
        c.criterionId?.toString() ?? '',
        c.criterionDescription ?? '',
        c.status ?? '',
        c.category ?? '',
        c.section ?? '',
        c.comment ?? ''
      ].join(' ').toLowerCase();

      return searchText.includes(term);
    });
  }

  get sortedFilteredCriteria(): AssessmentCriterionModel[] {
    const list = [...this.filteredCriteria];
    const direction = this.criteriaSortDirection === 'asc' ? 1 : -1;

    return list.sort((a, b) => {
      let result = 0;

      switch (this.criteriaSortColumn) {
        case 'criterionId':
          result = a.criterionId - b.criterionId;
          break;
        case 'criterionDescription':
          result = (a.criterionDescription ?? '').localeCompare(b.criterionDescription ?? '', 'fr', { sensitivity: 'base' });
          break;
        case 'status':
          result = (a.status ?? '').localeCompare(b.status ?? '', 'fr', { sensitivity: 'base' });
          break;
        case 'points':
          result = a.points - b.points;
          break;
        case 'isValidated':
          result = Number(a.isValidated) - Number(b.isValidated);
          break;
        case 'explanation':
          result = (a.explanation ?? '').localeCompare(b.explanation ?? '', 'fr', { sensitivity: 'base' });
          break;
        case 'comment':
          result = (a.comment ?? '').localeCompare(b.comment ?? '', 'fr', { sensitivity: 'base' });
          break;
      }

      if (result === 0) {
        result = a.criterionId - b.criterionId;
      }

      return result * direction;
    });
  }

  get showCategoryHeaders(): boolean {
    return this.criteriaSortColumn === 'criterionId' && this.criteriaSortDirection === 'asc';
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

  ngOnDestroy(): void {
    if (this.highlightTimeout) {
      clearTimeout(this.highlightTimeout);
      this.highlightTimeout = null;
    }
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

  getCriterionRowId(criterionId: number): string {
    return `criterion-row-${criterionId}`;
  }

  scrollToCriterion(criterionId: number): void {
    const row = document.getElementById(this.getCriterionRowId(criterionId));
    if (!row) return;

    row.scrollIntoView({ behavior: 'smooth', block: 'center' });
    this.highlightedCriterionId = criterionId;

    if (this.highlightTimeout) {
      clearTimeout(this.highlightTimeout);
    }

    this.highlightTimeout = setTimeout(() => {
      this.highlightedCriterionId = null;
      this.highlightTimeout = null;
    }, 2000);
  }

  toggleInvalidCriteriaByCategory(): void {
    this.showInvalidCriteriaByCategory = !this.showInvalidCriteriaByCategory;
  }

  onCriteriaSearch(value: string): void {
    this.criteriaSearchTerm = value;
  }

  clearCriteriaSearch(): void {
    this.criteriaSearchTerm = '';
  }

  setCriteriaSort(column: CriteriaSortColumn): void {
    if (this.criteriaSortColumn === column) {
      this.criteriaSortDirection = this.criteriaSortDirection === 'asc' ? 'desc' : 'asc';
      return;
    }

    this.criteriaSortColumn = column;
    this.criteriaSortDirection = 'asc';
  }

  getSortIconClass(column: CriteriaSortColumn): string {
    if (this.criteriaSortColumn !== column) return 'bi-arrow-down-up';
    return this.criteriaSortDirection === 'asc' ? 'bi-sort-up' : 'bi-sort-down';
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
