import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { FolderBllService } from '../../../../60_Bll/BackOffice/Folder-bll.service';
import { FolderModel } from '../../../../20_Models/BackOffice/Folder.model';
import { AuthenticateService } from '../../../../90_Services/Authenticate/Authenticate.service';

@Component({
  selector: 'app-front-dashboard',
  standalone: true,
  imports: [CommonModule, TranslationModule],
  templateUrl: './Dashboard-page.component.html',
  styleUrl: './Dashboard-page.component.css'
})
export class FrontDashboardComponent implements OnInit {

  loading = true;

  totalDossiers = 0;
  countInProgress = 0;
  countWaitingQuote = 0;
  countWaitingPayment = 0;
  countCompleted = 0;
  countCancelled = 0;

  completionRate = 0;
  inspectorFirstName = '';

  recentDossiers: FolderModel[] = [];

  private readonly STATUS_IN_PROGRESS = 1;
  private readonly STATUS_WAITING_QUOTE = 2;
  private readonly STATUS_WAITING_PAYMENT = 3;
  private readonly STATUS_COMPLETED = 4;
  private readonly STATUS_CANCELLED = 5;

  constructor(
    private folderBll: FolderBllService,
    private authService: AuthenticateService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const currentUser = this.authService.getCurrentUser();
    this.inspectorFirstName = currentUser?.firstName ?? '';
    const inspectorId = currentUser?.identifier ?? null;

    this.folderBll.getFolders$().subscribe({
      next: response => {
        const allFolders = response.folders ?? [];

        const myFolders = allFolders.filter(folder =>
          folder.inspectorUser?.identifier === inspectorId ||
          folder.inspectorUserIdentifier === inspectorId
        );

        this.totalDossiers = myFolders.length;

        this.countInProgress      = this.countByStatus(myFolders, this.STATUS_IN_PROGRESS);
        this.countWaitingQuote    = this.countByStatus(myFolders, this.STATUS_WAITING_QUOTE);
        this.countWaitingPayment  = this.countByStatus(myFolders, this.STATUS_WAITING_PAYMENT);
        this.countCompleted       = this.countByStatus(myFolders, this.STATUS_COMPLETED);
        this.countCancelled       = this.countByStatus(myFolders, this.STATUS_CANCELLED);

        this.completionRate = this.totalDossiers > 0
          ? Math.round((this.countCompleted / this.totalDossiers) * 100)
          : 0;

        this.recentDossiers = [...myFolders]
          .sort((a, b) => new Date(b.updatedDate).getTime() - new Date(a.updatedDate).getTime())
          .slice(0, 5);

        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  private countByStatus(folders: FolderModel[], statusId: number): number {
    return folders.filter(f => this.extractStatusId(f) === statusId).length;
  }

  private extractStatusId(folder: FolderModel): number | null {
    if (folder.folderStatusIdentifier != null) return folder.folderStatusIdentifier;
    if (folder.folderStatus != null) {
      const s = folder.folderStatus as any;
      return s?.identifier ?? s?.id ?? null;
    }
    return null;
  }

  getStatusLabel(folder: FolderModel): string {
    const id = this.extractStatusId(folder);
    switch (id) {
      case this.STATUS_IN_PROGRESS:      return 'FrontDossiersSection.StatusInProgress';
      case this.STATUS_WAITING_QUOTE:    return 'FrontDossiersSection.StatusWaitingQuote';
      case this.STATUS_WAITING_PAYMENT:  return 'FrontDossiersSection.StatusWaitingPayment';
      case this.STATUS_COMPLETED:        return 'FrontDossiersSection.StatusCompleted';
      case this.STATUS_CANCELLED:        return 'FrontDossiersSection.StatusCancelled';
      default:                           return 'CommonSection.NotProvided';
    }
  }

  getStatusClass(folder: FolderModel): string {
    const id = this.extractStatusId(folder);
    switch (id) {
      case this.STATUS_IN_PROGRESS:      return 'badge bg-primary';
      case this.STATUS_WAITING_QUOTE:    return 'badge bg-warning text-dark';
      case this.STATUS_WAITING_PAYMENT:  return 'badge bg-info text-dark';
      case this.STATUS_COMPLETED:        return 'badge bg-success';
      case this.STATUS_CANCELLED:        return 'badge bg-danger';
      default:                           return 'badge bg-secondary';
    }
  }

  goToDossiers(): void {
    this.router.navigate(['/fronthome/dossiers']);
  }
}
