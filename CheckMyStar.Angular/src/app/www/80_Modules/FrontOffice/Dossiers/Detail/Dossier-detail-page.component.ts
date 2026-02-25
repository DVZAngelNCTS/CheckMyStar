import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { switchMap } from 'rxjs/operators';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { FolderBllService } from '../../../../60_Bll/BackOffice/Folder-bll.service';
import { AppointmentBllService } from '../../../../60_Bll/BackOffice/Appointment-bll.service';
import { AddressBllService } from '../../../../60_Bll/BackOffice/Address-bll.service';
import { CountryBllService } from '../../../../60_Bll/BackOffice/Country-bll.service';
import { FolderModel } from '../../../../20_Models/BackOffice/Folder.model';
import { AppointmentModel } from '../../../../20_Models/BackOffice/Appointment.model';
import { AddressModel } from '../../../../20_Models/Common/Address.model';
import { CountryModel } from '../../../../20_Models/Common/Country.model';
import { TranslateService } from '@ngx-translate/core';
import { ToastService } from '../../../../90_Services/Toast/Toast.service';
import { PopupComponent } from '../../../Components/Popup/Popup.component';
import { AppointmentFormComponent } from './Form/Appointment-form.component';
import { EvaluationResultBllService } from '../../../../60_Bll/BackOffice/EvaluationResult-bll.service';
import { EvaluationResultModel } from '../../../../20_Models/BackOffice/EvaluationResult.model';
import { TooltipDirective } from '../../../Components/Tooltip/Tooltip.directive';

@Component({
  selector: 'app-dossier-detail-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, RouterModule, ReactiveFormsModule, PopupComponent, AppointmentFormComponent, TooltipDirective],
  templateUrl: './Dossier-detail-page.component.html',
  styleUrl: './Dossier-detail-page.component.css'
})
export class DossierDetailPageComponent implements OnInit {
  loading = false;
  folder: FolderModel | null = null;
  folderId: number | null = null;
  countries: CountryModel[] = [];

  // Rendez-vous
  appointment: AppointmentModel | null = null;
  loadingAppointment = false;

  // Popup rendez-vous (création / modification)
  popupVisible = false;
  popupLoading = false;
  popupError: string | null = null;
  isEditMode = false;
  appointmentForm!: FormGroup;

  // Pays sélectionné (stocké localement car le backend ne retourne pas country dans l'adresse)
  storedCountryIdentifier: number | null = null;

  // Popup suppression rendez-vous
  deletePopupVisible = false;
  deletePopupLoading = false;
  deletePopupError: string | null = null;

  // Historique des évaluations
  assessmentResults: EvaluationResultModel[] = [];
  loadingHistory = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private folderBll: FolderBllService,
    private appointmentBll: AppointmentBllService,
    private addressBll: AddressBllService,
    private countryBll: CountryBllService,
    private evaluationResultBll: EvaluationResultBllService,
    private translate: TranslateService,
    private toast: ToastService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.folderId = id ? +id : null;

    this.appointmentForm = this.fb.group({
      appointmentDate: [null, Validators.required],
      comment: [null],
      addressNumber: [null],
      addressLine: [null, Validators.required],
      city: [null, Validators.required],
      zipCode: [null, Validators.required],
      region: [null],
      countryIdentifier: [null, Validators.required]
    });

    this.countryBll.getCountries$().subscribe({
      next: resp => this.countries = resp.countries ?? []
    });

    if (!this.folderId) return;

    // Priorité : données transmises par la liste via router state (évite un appel API)
    const stateFolder = history.state?.folder as FolderModel | null | undefined;
    if (stateFolder && stateFolder.identifier === this.folderId) {
      this.folder = stateFolder;
    } else {
      this.loadFolder();
    }

    this.loadAppointment();
    this.loadAssessmentHistory();
  }

  loadFolder(): void {
    this.loading = true;
    this.folderBll.getfolder$(this.folderId!).subscribe({
      next: response => {
        this.folder = response.folder ?? null;
        this.loading = false;
      },
      error: err => {
        this.loading = false;
        console.error(err);
        this.toast.show(this.translate.instant('CommonSection.UnknownError'), 'error', 5000);
      }
    });
  }

  loadAppointment(): void {
    if (!this.folderId) return;
    this.loadingAppointment = true;
    this.appointmentBll.getAppointmentByFolder$(this.folderId).subscribe({
      next: response => {
        this.appointment = response.appointment ?? null;
        this.loadingAppointment = false;
      },
      error: () => {
        this.appointment = null;
        this.loadingAppointment = false;
      }
    });
  }

  loadAssessmentHistory(): void {
    if (!this.folderId) return;
    this.loadingHistory = true;
    this.evaluationResultBll.getAssessmentResultsByFolder$(this.folderId).subscribe({
      next: response => {
        this.assessmentResults = response.assessmentResults ?? [];
        this.loadingHistory = false;
      },
      error: () => {
        this.assessmentResults = [];
        this.loadingHistory = false;
      }
    });
  }

  onViewEvaluationDetail(result: EvaluationResultModel): void {
    if (!this.folderId) return;
    this.router.navigate(['/fronthome/dossiers', this.folderId, 'evaluation-view'], {
      state: { result }
    });
  }

  openCreateAppointment(): void {
    this.isEditMode = false;
    this.appointmentForm.reset();
    this.popupError = null;
    this.popupVisible = true;
  }

  openEditAppointment(): void {
    if (!this.appointment) return;
    this.isEditMode = true;
    this.popupError = null;

    const addr = this.appointment.address;
    const countryId = addr?.country?.identifier ?? this.storedCountryIdentifier ?? null;
    this.appointmentForm.patchValue({
      appointmentDate: this.appointment.appointmentDate ?? null,
      comment: this.appointment.comment ?? null,
      addressNumber: addr?.number ?? null,
      addressLine: addr?.addressLine ?? null,
      city: addr?.city ?? null,
      zipCode: addr?.zipCode ?? null,
      region: addr?.region ?? null,
      countryIdentifier: countryId
    });

    this.popupVisible = true;
  }

  openDeleteAppointment(): void {
    this.deletePopupError = null;
    this.deletePopupVisible = true;
  }

  onDeletePopupCancel(): void {
    this.deletePopupVisible = false;
    this.deletePopupError = null;
  }

  onDeletePopupConfirm(): void {
    if (this.appointment == null || this.appointment.identifier == null) return;

    this.deletePopupLoading = true;
    this.deletePopupError = null;

    this.appointmentBll.deleteAppointment$(this.appointment.identifier, this.folderId!).subscribe({
      next: response => {
        this.deletePopupLoading = false;
        if ((response as any)?.isSuccess !== false) {
          this.appointment = null;
          if (this.folder) this.folder.appointment = null;
          this.deletePopupVisible = false;
          this.toast.show(this.translate.instant('DossierDetailSection.AppointmentDeleted'), 'success', 3000);
        } else {
          this.deletePopupError = (response as any)?.message || this.translate.instant('CommonSection.UnknownError');
        }
      },
      error: err => {
        this.deletePopupLoading = false;
        console.error(err);
        this.deletePopupError = err?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  onPopupCancel(): void {
    this.popupVisible = false;
    this.popupError = null;
  }

  onPopupConfirm(): void {
    if (this.appointmentForm.invalid) {
      this.appointmentForm.markAllAsTouched();
      this.popupError = this.translate.instant('ValidationSection.Required');
      return;
    }

    if (this.isEditMode) {
      this.onEditAppointmentConfirm();
    } else {
      this.onCreateAppointmentConfirm();
    }
  }

  private onEditAppointmentConfirm(): void {
    this.popupLoading = true;
    this.popupError = null;

    const f = this.appointmentForm.value;
    const selectedCountry = this.countries.find(c => c.identifier === +f.countryIdentifier)
      ?? { identifier: +f.countryIdentifier, name: '', code: '' };

    const existingAddress = this.appointment?.address;
    const address: AddressModel = {
      identifier: existingAddress?.identifier ?? 0,
      number: f.addressNumber || '',
      addressLine: f.addressLine,
      city: f.city,
      zipCode: f.zipCode,
      region: f.region || undefined,
      country: selectedCountry
    };

    const appointmentModel: AppointmentModel = {
      identifier: this.appointment?.identifier,
      appointmentDate: f.appointmentDate,
      comment: f.comment || null,
      address
    };

    this.appointmentBll.updateAppointment$(appointmentModel, this.folderId!).subscribe({
      next: response => {
        this.popupLoading = false;
        if (response?.isSuccess) {
          this.storedCountryIdentifier = +f.countryIdentifier;
          this.loadAppointment();
          this.popupVisible = false;
          this.toast.show(this.translate.instant('DossierDetailSection.AppointmentUpdated'), 'success', 3000);
        } else {
          this.popupError = response?.message || this.translate.instant('CommonSection.UnknownError');
        }
      },
      error: err => {
        this.popupLoading = false;
        console.error(err);
        this.popupError = err?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  private onCreateAppointmentConfirm(): void {
    this.popupLoading = true;
    this.popupError = null;

    const f = this.appointmentForm.value;
    const selectedCountry = this.countries.find(c => c.identifier === +f.countryIdentifier)
      ?? { identifier: +f.countryIdentifier, name: '', code: '' };

    // Étape 1 : obtenir le prochain identifiant d'adresse
    this.addressBll.getNextIdentifier$().pipe(
      switchMap(addrIdResp => {
        const addressId = addrIdResp.address?.identifier;
        if (addressId == null) throw new Error(this.translate.instant('CommonSection.UnknownError'));

        const address: AddressModel = {
          identifier: addressId,
          number: f.addressNumber || '',
          addressLine: f.addressLine,
          city: f.city,
          zipCode: f.zipCode,
          region: f.region || undefined,
          country: selectedCountry
        };

        // Étape 2 : créer l'adresse
        return this.addressBll.addAddress$({ address }).pipe(
          switchMap(addrResp => {
            if (!addrResp.isSuccess) throw new Error(addrResp.message || this.translate.instant('CommonSection.UnknownError'));

            // Étape 3 : obtenir le prochain identifiant de rendez-vous
            return this.appointmentBll.getNextIdentifier$().pipe(
              switchMap(apptIdResp => {
                const appointmentId = apptIdResp.identifier;

                const appointmentModel: AppointmentModel = {
                  identifier: appointmentId,
                  appointmentDate: f.appointmentDate,
                  comment: f.comment || null,
                  address: { ...address }
                };

                // Étape 4 : créer le rendez-vous
                return this.appointmentBll.addAppointment$(appointmentModel, this.folderId!);
              })
            );
          })
        );
      })
    ).subscribe({
      next: response => {
        this.popupLoading = false;
        if (response?.isSuccess) {
          this.storedCountryIdentifier = +f.countryIdentifier;
          this.loadAppointment();
          this.popupVisible = false;
          this.toast.show(this.translate.instant('DossierDetailSection.AppointmentCreated'), 'success', 3000);
        } else {
          this.popupError = response?.message || this.translate.instant('CommonSection.UnknownError');
        }
      },
      error: err => {
        this.popupLoading = false;
        console.error(err);
        this.popupError = err?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  getStatusLabel(): string {
    const status = this.folder?.folderStatus;
    if (status == null) return this.translate.instant('CommonSection.NotProvided');

    let numericStatus: number | null = null;

    if (typeof status === 'object' && status !== null && 'identifier' in status) {
      const id = (status as any).identifier;
      if (typeof id === 'number') numericStatus = id;
      else if (typeof id === 'string') {
        const parsed = parseInt(id, 10);
        if (!isNaN(parsed)) numericStatus = parsed;
      }
    } else if (typeof status === 'number') {
      numericStatus = status;
    } else if (typeof status === 'string') {
      const parsed = parseInt(status, 10);
      if (!isNaN(parsed)) numericStatus = parsed;
    }

    if (numericStatus !== null) {
      const statusMap: { [key: number]: string } = {
        1: 'FrontDossiersSection.StatusInProgress',
        2: 'FrontDossiersSection.StatusWaitingQuote',
        3: 'FrontDossiersSection.StatusWaitingPayment',
        4: 'FrontDossiersSection.StatusCompleted',
        5: 'FrontDossiersSection.StatusCancelled'
      };
      const key = statusMap[numericStatus];
      return key ? this.translate.instant(key) : numericStatus.toString();
    }

    return this.translate.instant('CommonSection.NotProvided');
  }

  getStatusBadgeClass(): string {
    const status = this.folder?.folderStatus;
    let numericStatus: number | null = null;

    if (typeof status === 'object' && status !== null && 'identifier' in status) {
      numericStatus = (status as any).identifier ?? null;
    } else if (typeof status === 'number') {
      numericStatus = status;
    } else if (typeof status === 'string') {
      numericStatus = parseInt(status, 10) || null;
    }

    switch (numericStatus) {
      case 1: return 'bg-primary';
      case 2: return 'bg-warning text-dark';
      case 3: return 'bg-danger';
      case 4: return 'bg-success';
      case 5: return 'bg-secondary';
      default: return 'bg-secondary';
    }
  }

  getAppointmentDate(): string | null {
    // Priorité à l'objet chargé via l'API Appointment
    if (this.appointment) {
      return this.appointment.appointmentDate ?? null;
    }

    // Fallback sur les données du dossier
    const folderAppointment = this.folder?.appointment;
    if (!folderAppointment) return null;

    if (typeof folderAppointment === 'object' && folderAppointment !== null) {
      const appt = folderAppointment as any;
      return appt.date ?? appt.appointmentDate ?? appt.scheduledDate ?? appt.appointmentDateTime ?? null;
    }

    if (typeof folderAppointment === 'string') return folderAppointment;

    return null;
  }

  getCivilityLabel(civility: number): string {
    if (civility === 1) return this.translate.instant('UserSection.Mister');
    if (civility === 2) return this.translate.instant('UserSection.Madam');
    return '';
  }

  onViewInvoice(): void {
    this.toast.show(this.translate.instant('DossierDetailSection.InvoiceNotAvailable'), 'info', 4000);
  }

  onViewQuote(): void {
    this.toast.show(this.translate.instant('DossierDetailSection.QuoteNotAvailable'), 'info', 4000);
  }

  onEvaluate(): void {
    if (this.folderId) {
      this.router.navigate(['/fronthome/dossiers', this.folderId, 'evaluation']);
    }
  }
}
