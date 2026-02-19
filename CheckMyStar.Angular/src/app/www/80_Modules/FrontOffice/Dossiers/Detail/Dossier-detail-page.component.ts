import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { FolderBllService } from '../../../../60_Bll/BackOffice/Folder-bll.service';
import { FolderModel } from '../../../../20_Models/BackOffice/Folder.model';
import { TranslateService } from '@ngx-translate/core';
import { ToastService } from '../../../../90_Services/Toast/Toast.service';

@Component({
  selector: 'app-dossier-detail-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, RouterModule],
  templateUrl: './Dossier-detail-page.component.html',
  styleUrl: './Dossier-detail-page.component.css'
})
export class DossierDetailPageComponent implements OnInit {
  loading = false;
  folder: FolderModel | null = null;
  folderId: number | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private folderBll: FolderBllService,
    private translate: TranslateService,
    private toast: ToastService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.folderId = id ? +id : null;

    if (!this.folderId) return;

    // Priorité : données transmises par la liste via router state (évite un appel API)
    const stateFolder = history.state?.folder as FolderModel | null | undefined;
    if (stateFolder && stateFolder.identifier === this.folderId) {
      this.folder = stateFolder;
    } else {
      this.loadFolder();
    }
  }

  loadFolder(): void {
    this.loading = true;
    this.folderBll.getFolders$().subscribe({
      next: response => {
        const allFolders = response.folders ?? [];
        this.folder = allFolders.find(f => f.identifier === this.folderId) ?? null;
        this.loading = false;
      },
      error: err => {
        this.loading = false;
        console.error(err);
        this.toast.show(this.translate.instant('CommonSection.UnknownError'), 'error', 5000);
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
    const appointment = this.folder?.appointment;
    if (!appointment) return null;

    if (typeof appointment === 'object' && appointment !== null) {
      const appt = appointment as any;
      return appt.date ?? appt.appointmentDate ?? appt.scheduledDate ?? appt.appointmentDateTime ?? null;
    }

    if (typeof appointment === 'string') return appointment;

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
