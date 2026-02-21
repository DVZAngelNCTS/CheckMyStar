import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { CriteresBllService } from '../../../../60_Bll/BackOffice/Criteres-bll.service';
import { AssessmentBllService } from '../../../../60_Bll/BackOffice/Assessment-bll.service';
import { TranslateService } from '@ngx-translate/core';
import { ToastService } from '../../../../90_Services/Toast/Toast.service';
import { EvaluationStep1Component } from './Step1/Evaluation-step1.component';
import { EvaluationStep2Component } from './Step2/Evaluation-step2.component';
import { EvaluationResult, EvaluationFormData, CriterionEvaluation } from "../../../../20_Models/FrontOffice/Evaluation.models";
import { AssessmentModel } from '../../../../20_Models/BackOffice/Assessment.model';
import { AssessmentCriterionModel } from '../../../../20_Models/BackOffice/AssessmentCriterion.model';

@Component({
  selector: 'app-evaluation-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, RouterModule, EvaluationStep1Component, EvaluationStep2Component],
  templateUrl: './Evaluation-page.component.html',
  styleUrl: './Evaluation-page.component.css'
})
export class EvaluationPageComponent implements OnInit {
  folderId: number | null = null;
  step = 1;
  loading = false;
  result: EvaluationResult | null = null;
  lastSavedAt: string | null = null;
  draftAssessmentId: number | null = null;

  private get storageKey(): string {
    return `evaluation_draft_${this.folderId}`;
  }

  form: EvaluationFormData = {
    targetStar: null,
    maxCapacity: null,
    floors: null,
    isZoneBlanche: false,
    isDromTom: false,
    isHauteMontagne: false,
    isBatimentClasse: false,
    isStudioSansSejouur: false,
    isStationnementImpossible: false,
    totalArea: null,
    roomCount: null,
    totalRoomsArea: null,
    smallestRoomArea: null
  };

  criteriaEvaluations: CriterionEvaluation[] = [];

  constructor(
    private route: ActivatedRoute,
    private criteresBll: CriteresBllService,
    private assessmentBll: AssessmentBllService,
    private translate: TranslateService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.folderId = id ? +id : null;
    this.tryRestoreDraft();
  }

  private tryRestoreDraft(): void {
    try {
      const raw = localStorage.getItem(this.storageKey);
      if (!raw) return;
      const draft = JSON.parse(raw);
      if (draft?.form) this.form = draft.form;
      if (draft?.criteriaEvaluations?.length) {
        this.criteriaEvaluations = draft.criteriaEvaluations;
        this.step = draft.step === 2 ? 2 : 1;
      }
      if (draft?.savedAt) {
        this.lastSavedAt = new Date(draft.savedAt).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
      }
      if (draft?.draftAssessmentId) {
        this.draftAssessmentId = draft.draftAssessmentId;
      }
      if (draft?.criteriaEvaluations?.length) {
        this.toast.show(this.translate.instant('EvaluationSection.SaveRestored'), 'success', 3000);
      }
    } catch { /* brouillon corrompu — on ignore */ }
  }

  onSave(): void {
    const savedAt = new Date().toISOString();
    this.assessmentBll.addAssessment$(this.buildAssessmentModel(false)).subscribe({
      next: (response) => {
        if (response?.assessmentId) {
          this.draftAssessmentId = response.assessmentId;
        }
        this.saveToLocalStorage(savedAt);
        this.toast.show(this.translate.instant('EvaluationSection.SaveSuccess'), 'success', 2500);
      },
      error: err => {
        console.error('Assessment save error:', err);
        this.saveToLocalStorage(savedAt);
        this.toast.show(this.translate.instant('EvaluationSection.SaveApiError'), 'warning', 4000);
      }
    });
  }

  private buildAssessmentModel(isComplete: boolean) {
    const assessment: AssessmentModel = {
      folderIdentifier:     this.folderId ?? 0,
      targetStarLevel:      this.form.targetStar ?? 0,
      capacity:             this.form.maxCapacity ?? 0,
      numberOfFloors:       this.form.floors ?? 0,
      isWhiteZone:          this.form.isZoneBlanche,
      isDromTom:            this.form.isDromTom,
      isHighMountain:       this.form.isHauteMontagne,
      isBuildingClassified: this.form.isBatimentClasse,
      isStudioNoLivingRoom: this.form.isStudioSansSejouur,
      isParkingImpossible:  this.form.isStationnementImpossible,
      totalArea:            this.form.totalArea        ?? 0,
      numberOfRooms:        this.form.roomCount         ?? 0,
      totalRoomsArea:       this.form.totalRoomsArea    ?? 0,
      smallestRoomArea:     this.form.smallestRoomArea  ?? 0,
      isComplete,
      criteria: this.criteriaEvaluations.map(c => ({
        criterionId: c.criterionId,
        points:      c.basePoints,
        status:      c.typeCode,
        isValidated: c.validated,
        comment:     c.comment
      }) as AssessmentCriterionModel)
    };
    return assessment;
  }

  private saveToLocalStorage(savedAt: string): void {
    try {
      const draft = {
        folderId: this.folderId,
        savedAt,
        step: this.step,
        form: this.form,
        criteriaEvaluations: this.criteriaEvaluations,
        draftAssessmentId: this.draftAssessmentId
      };
      localStorage.setItem(this.storageKey, JSON.stringify(draft));
      this.lastSavedAt = new Date(savedAt).toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    } catch { /* storage indisponible */ }
  }

  onStep1Validated(): void {
    this.loading = true;
    this.criteresBll.getStarCriteriaDetails$().subscribe({
      next: response => {
        const star = response.starCriterias?.find(s => s.rating === this.form.targetStar);
        if (star) {
          this.criteriaEvaluations = star.criteria.map(c => ({
            criterionId: c.criterionId,
            description: c.description,
            basePoints: c.basePoints,
            typeCode: c.typeCode,
            typeLabel: c.typeLabel,
            validated: false,
            comment: ''
          }));
          this.step = 2;
        } else {
          this.toast.show(this.translate.instant('EvaluationSection.NoCriteriaFound'), 'error', 4000);
        }
        this.loading = false;
      },
      error: err => {
        this.loading = false;
        console.error(err);
        this.toast.show(this.translate.instant('CommonSection.UnknownError'), 'error', 5000);
      }
    });
  }

  onGoBack(): void {
    this.step = 1;
  }

  onSubmit(): void {
    const criteria = this.criteriaEvaluations;
    const stars = this.form.targetStar ?? 0;

    // ── Critères obligatoires : X et X_ONC ──────────────────────────────────
    const mandatory = criteria.filter(c =>
      c.typeCode.toUpperCase() === 'X' || c.typeCode.toUpperCase() === 'X_ONC'
    );
    const totalMandatoryPoints = mandatory.reduce((s, c) => s + c.basePoints, 0);
    const mandatoryThreshold   = Math.ceil(totalMandatoryPoints * 0.95);
    const earnedMandatoryPoints = mandatory
      .filter(c => c.validated)
      .reduce((s, c) => s + c.basePoints, 0);

    // ── Critères ONC : tous doivent être cochés ──────────────────────────────
    const oncCriteria    = criteria.filter(c => c.typeCode.toUpperCase() === 'X_ONC');
    const oncFailedCount = oncCriteria.filter(c => !c.validated).length;
    const oncOk          = oncFailedCount === 0;

    // ── Critères optionnels : O ──────────────────────────────────────────────
    const optional           = criteria.filter(c => c.typeCode.toUpperCase() === 'O');
    const totalOptionalPoints = optional.reduce((s, c) => s + c.basePoints, 0);

    const optionalPercentages: Record<number, number> = { 1: 0.05, 2: 0.10, 3: 0.20, 4: 0.30, 5: 0.40 };
    const optionalPct         = optionalPercentages[stars] ?? 0;
    const minOptionalRequired = Math.ceil(totalOptionalPoints * optionalPct);

    const missingMandatory = Math.max(0, mandatoryThreshold - earnedMandatoryPoints);
    const bonus            = missingMandatory * 3;
    const requiredOptional = minOptionalRequired + bonus;

    const earnedOptionalPoints = optional
      .filter(c => c.validated)
      .reduce((s, c) => s + c.basePoints, 0);

    // ── Décision ─────────────────────────────────────────────────────────────
    const mandatoryOk = earnedMandatoryPoints >= mandatoryThreshold;
    const optionalOk  = earnedOptionalPoints  >= requiredOptional;
    const accepted    = mandatoryOk && optionalOk && oncOk;

    this.result = {
      accepted,
      mandatoryOk,
      optionalOk,
      oncOk,
      earnedMandatoryPoints,
      mandatoryThreshold,
      totalMandatoryPoints,
      earnedOptionalPoints,
      requiredOptional,
      totalOptionalPoints,
      minOptionalRequired,
      bonus,
      missingMandatory,
      oncCount: oncCriteria.length,
      oncFailedCount
    };

    // ── Persistance finale (isComplete = true) ──────────────────────────
    if (this.draftAssessmentId !== null) {
      // Supprimer le brouillon BDD avant de créer l'évaluation finale
      const draftId = this.draftAssessmentId;
      this.assessmentBll.deleteAssessment$(draftId).subscribe({
        next: () => {
          this.draftAssessmentId = null;
          this.assessmentBll.addAssessment$(this.buildAssessmentModel(true)).subscribe({
            next: () => {
              localStorage.removeItem(this.storageKey);
              this.lastSavedAt = null;
            },
            error: err => console.error('Assessment submit error:', err)
          });
        },
        error: err => {
          console.error('Draft delete error:', err);
          // On tente quand même de soumettre la version finale
          this.assessmentBll.addAssessment$(this.buildAssessmentModel(true)).subscribe({
            next: () => {
              localStorage.removeItem(this.storageKey);
              this.lastSavedAt = null;
            },
            error: e => console.error('Assessment submit error:', e)
          });
        }
      });
    } else {
      this.assessmentBll.addAssessment$(this.buildAssessmentModel(true)).subscribe({
        next: () => {
          localStorage.removeItem(this.storageKey);
          this.lastSavedAt = null;
        },
        error: err => console.error('Assessment submit error:', err)
      });
    }

    this.step = 3;
  }

  onRestart(): void {
    this.result = null;
    this.step = 1;
    this.criteriaEvaluations = [];
    this.lastSavedAt = null;
    this.draftAssessmentId = null;
    localStorage.removeItem(this.storageKey);
    this.form = {
      targetStar: null,
      maxCapacity: null,
      floors: null,
      isZoneBlanche: false,
      isDromTom: false,
      isHauteMontagne: false,
      isBatimentClasse: false,
      isStudioSansSejouur: false,
      isStationnementImpossible: false,
      totalArea: null,
      roomCount: null,
      totalRoomsArea: null,
      smallestRoomArea: null
    };
  }
}
