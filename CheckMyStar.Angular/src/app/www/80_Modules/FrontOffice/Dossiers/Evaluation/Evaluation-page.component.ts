import { Component, OnInit } from '@angular/core';
import { EMPTY } from 'rxjs';
import { switchMap } from 'rxjs/operators';
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
import { EvaluationResultBllService } from '../../../../60_Bll/BackOffice/EvaluationResult-bll.service';
import { TooltipDirective } from '../../../Components/Tooltip/Tooltip.directive';

@Component({
  selector: 'app-evaluation-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, RouterModule, EvaluationStep1Component, EvaluationStep2Component, TooltipDirective],
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
    isLocalisationNonAdaptee: false,
    isPasDeTri: false,
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
    private evaluationResultBll: EvaluationResultBllService,
    private translate: TranslateService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.folderId = id ? +id : null;
    this.tryRestoreDraft();
  }

  private tryRestoreDraft(): void {
    let restoredLocally = false;
    try {
      const raw = localStorage.getItem(this.storageKey);
      if (raw) {
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
        restoredLocally = true;
      }
    } catch { /* brouillon corrompu — on ignore */ }

    if (!restoredLocally) {
      this.loadDraftFromServer();
    }
  }

  /** Fallback : récupère le brouillon depuis la base de données (autre appareil). */
  private loadDraftFromServer(): void {
    if (!this.folderId) return;

    this.assessmentBll.getAssessmentByFolder$(this.folderId).pipe(
      switchMap(response => {
        const assessment = response?.assessment;
        if (!assessment || assessment.isComplete) return EMPTY;

        this.draftAssessmentId = assessment.identifier;
        this.form = this.mapAssessmentToForm(assessment);

        return this.assessmentBll.getAssessmentCriteria$(assessment.identifier);
      })
    ).subscribe({
      next: criteriaResponse => {
        if (criteriaResponse?.assessmentCriteria?.length) {
          this.criteriaEvaluations = this.mapCriteriaToEvaluations(criteriaResponse.assessmentCriteria);
          this.step = 2;
        }
        this.toast.show(this.translate.instant('EvaluationSection.SaveRestored'), 'success', 3000);
      },
      error: err => console.error('Could not load draft from server:', err)
    });
  }

  private mapAssessmentToForm(assessment: AssessmentModel): EvaluationFormData {
    return {
      targetStar:               assessment.targetStarLevel  ?? null,
      maxCapacity:              assessment.capacity         ?? null,
      floors:                   assessment.numberOfFloors   ?? null,
      isZoneBlanche:            assessment.isWhiteZone,
      isDromTom:                assessment.isDromTom,
      isHauteMontagne:          assessment.isHighMountain,
      isBatimentClasse:         assessment.isBuildingClassified,
      isStudioSansSejouur:      assessment.isStudioNoLivingRoom,
      isStationnementImpossible: assessment.isParkingImpossible,
      isLocalisationNonAdaptee: false,   // non stocké en base
      isPasDeTri:               false,   // non stocké en base
      totalArea:                assessment.totalArea        ?? null,
      roomCount:                assessment.numberOfRooms    ?? null,
      totalRoomsArea:           assessment.totalRoomsArea   ?? null,
      smallestRoomArea:         assessment.smallestRoomArea ?? null
    };
  }

  private mapCriteriaToEvaluations(criteria: AssessmentCriterionModel[]): CriterionEvaluation[] {
    return criteria.map(c => ({
      criterionId: c.criterionId,
      description: c.criterionDescription,
      basePoints:  c.basePoints,
      typeCode:    c.status,
      typeLabel:   '',
      validated:   c.isValidated,
      comment:     c.comment
    }));
  }

  onSave(): void {
    const savedAt = new Date().toISOString();
    const assessmentModel = this.buildAssessmentModel(false);

    const saveObservable = this.draftAssessmentId !== null
      ? this.assessmentBll.updateAssessment$(assessmentModel)
      : this.assessmentBll.addAssessment$(assessmentModel);

    saveObservable.subscribe({
      next: (response) => {
        if (response?.assessment?.identifier && this.draftAssessmentId === null) {
          this.draftAssessmentId = response.assessment.identifier;
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
      identifier:           this.draftAssessmentId ?? 0,
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
          const raw = star.criteria.map(c => ({
            criterionId: c.criterionId,
            description: c.description,
            basePoints: c.basePoints,
            typeCode: c.typeCode,
            typeLabel: c.typeLabel,
            validated: false,
            comment: ''
          }));
          this.criteriaEvaluations = this.applyConstraints(raw);
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

  private applyConstraints(evaluations: CriterionEvaluation[]): CriterionEvaluation[] {
    const form = this.form;
    const stars    = form.targetStar    ?? 0;
    const capacity = form.maxCapacity   ?? 0;
    const floors   = form.floors        ?? 0;

    return evaluations.map(c => {
      const id = c.criterionId;
      let typeCode = c.typeCode;

      // Zone blanche → C5, C6, C7 NA
      if (form.isZoneBlanche && [5, 6, 7].includes(id)) typeCode = 'NA';

      // DROM-COM → C16, C19 NA
      if (form.isDromTom && [16, 19].includes(id)) typeCode = 'NA';

      // Haute montagne / Saint-Pierre-et-Miquelon → C17 NA
      if (form.isHauteMontagne && id === 17) typeCode = 'NA';

      // Bâtiment classé → C15, C77, C78 NA
      if (form.isBatimentClasse && [15, 77, 78].includes(id)) typeCode = 'NA';

      // Studio / bien sans séjour → C26, C27 NA
      if (form.isStudioSansSejouur && [26, 27].includes(id)) typeCode = 'NA';

      // Réglementation locale (stationnement impossible) → C79, C80 NA
      if (form.isStationnementImpossible && [79, 80].includes(id)) typeCode = 'NA';

      // ── Capacité d'accueil ──────────────────────────────────────────────────
      // C18 (machine à laver) : NA si < 4 personnes
      if (id === 18 && capacity < 4) typeCode = 'NA';

      // C19 (sèche-linge) : NA si < 6 personnes
      if (id === 19 && capacity < 6) typeCode = 'NA';

      // C43, C44, C45 (2e salle d'eau) :
      //   1–4 étoiles → NA si < 7 personnes ; 5 étoiles → NA si < 5 personnes
      if ([43, 44, 45].includes(id)) {
        if (stars <= 4 && capacity < 7) typeCode = 'NA';
        else if (stars === 5 && capacity < 5) typeCode = 'NA';
      }

      // C72 (lave-vaisselle) : NA si < 2 personnes
      if (id === 72 && capacity < 2) typeCode = 'NA';

      // C73 (lave-vaisselle 6 couverts) : NA si < 4 personnes
      if (id === 73 && capacity < 4) typeCode = 'NA';

      // ── Nombre d'étages (priorité : NA bâtiment classé prime) ───────────────
      if (typeCode !== 'NA') {
        // C77 (ascenseur 4e étage) : NA en RDC, O pour étages 1–3
        if (id === 77) {
          if (floors === 0) typeCode = 'NA';
          else if (floors >= 1 && floors <= 3) typeCode = 'O';
        }
        // C78 (ascenseur 3e étage) : NA en RDC, O pour étages 1–2
        if (id === 78) {
          if (floors === 0) typeCode = 'NA';
          else if (floors >= 1 && floors <= 2) typeCode = 'O';
        }
      }

      // Localisation non adaptée → C91 NA
      if (form.isLocalisationNonAdaptee && id === 91) typeCode = 'NA';

      // Pas de tri sélectif dans la commune → C128 NA
      if (form.isPasDeTri && id === 128) typeCode = 'NA';

      return { ...c, typeCode };
    });
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

    // ── Persistance finale (isComplete = true) + sauvegarde du résultat ───
    const finalAssessmentModel = this.buildAssessmentModel(true);
    const saveObservable = this.draftAssessmentId !== null
      ? this.assessmentBll.updateAssessment$(finalAssessmentModel)
      : this.assessmentBll.addAssessment$(finalAssessmentModel);

    saveObservable.pipe(
      switchMap(response => {
        const assessmentId = response?.assessment?.identifier ?? this.draftAssessmentId ?? 0;
        return this.evaluationResultBll.saveEvaluationResult$({
          assessmentIdentifier:  assessmentId,
          isAccepted:            accepted,
          mandatoryPointsEarned: earnedMandatoryPoints,
          mandatoryThreshold,
          optionalPointsEarned:  earnedOptionalPoints,
          optionalRequired:      requiredOptional,
          onceFailedCount:       oncFailedCount
        });
      })
    ).subscribe({
      next: () => {
        localStorage.removeItem(this.storageKey);
        this.lastSavedAt = null;
        this.draftAssessmentId = null;
      },
      error: err => console.error('EvaluationResult submit error:', err)
    });

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
      isLocalisationNonAdaptee: false,
      isPasDeTri: false,
      totalArea: null,
      roomCount: null,
      totalRoomsArea: null,
      smallestRoomArea: null
    };
  }
}
