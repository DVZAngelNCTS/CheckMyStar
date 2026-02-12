import { Component, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { CriteresBllService } from '../../../../60_Bll/BackOffice/Criteres-bll.service';
import { StarCriteria, StarCriterionDetail } from '../../../../20_Models/BackOffice/Criteres.model';
import { CriteresFilterComponent } from './Filter/Criteres-filter.component';
import { CriteresFormComponent } from './Form/Criteres-form.component';
import { TableComponent } from '../../../Components/Table/Table.component';
import { TableColumn } from '../../../Components/Table/Models/TableColumn.model';
import { PopupComponent } from '../../../Components/Popup/Popup.component';
import { RatingContextService } from '../Service/Rating-context.service';
import { ToastService } from '../../../../90_Services/Toast/Toast.service';
import { HttpErrorResponse } from '@angular/common/http';
import { TranslateService } from '@ngx-translate/core';
import { CreateCriterionRequest, UpdateCriterionRequest } from '../../../../20_Models/BackOffice/Criteres.model';

@Component({
  selector: 'app-criteres-management-page',
  standalone: true,
  imports: 
  [
    CommonModule, 
    RouterModule, 
    TranslationModule,
    CriteresFilterComponent,
    CriteresFormComponent,
    TableComponent,
    PopupComponent
  ],
  templateUrl: './Criteres-management-page.component.html',
  styleUrl: './Criteres-management-page.component.css'
})
export class CriteresManagementPageComponent implements OnInit 
{
  @ViewChild(CriteresFormComponent) formComponent!: CriteresFormComponent;
  
  selectedStar: StarCriteria | null = null;
  allCriteria: StarCriterionDetail[] = [];
  filteredCriteria: StarCriterionDetail[] = [];
  starRating: number = 0;
  
  loadingSearch = false;
  loadingReset = false;
  
  columns: TableColumn<StarCriterionDetail>[] = 
  [
    { field: 'criterionId', header: 'Id', icon: '', sortable: true, width: '80px' },
    { field: 'description', header: 'Description', icon: '', sortable: true },
    { field: 'basePoints', header: 'Points', icon: '', sortable: true, width: '100px' },
    { field: 'typeCode', header: 'Type', icon: '', sortable: true, width: '120px' }
  ];

  // Popup state
  showPopup = false;
  popupTitle = '';
  popupMode: 'create' | 'edit' | 'delete' | null = null;
  popupConfirmLabel = '';
  popupCancelLabel = '';
  popupError: string | null = null;
  loading = false;
  popupWidth = '600px';
  popupHeight = 'auto';
  currentCriterion: StarCriterionDetail | null = null;
  isEdit = false;

  constructor(
    private criteresBll: CriteresBllService, 
    private route: ActivatedRoute, 
    private router: Router, 
    private ratingContext: RatingContextService,
    private translate: TranslateService,
    private toast: ToastService  
  ) 
  {}

  ngOnInit(): void 
  {
    this.starRating = this.ratingContext.rating || 0;
    if (!this.starRating) 
    {
      this.router.navigate(['/backhome/criteres']);
      return;
    }
    this.loadData();
  }

  private loadData(): void 
  {
    this.criteresBll.getStarCriterias$().subscribe(
    {
      next: stars => 
      {
        this.selectedStar = stars.starCriterias?.find(s => s.rating === this.starRating) || null;
      },
      error: err => console.error('Erreur getStarCriteria', err)
    });

    this.criteresBll.getStarCriteriaDetails$().subscribe(
    {
      next: details => 
        {
        const detailsForStar = details.starCriterias?.find(d => d.rating === this.starRating);
        this.allCriteria = detailsForStar ? detailsForStar.criteria : [];
        this.filteredCriteria = [...this.allCriteria];
      },
      error: err => console.error('Erreur getStarCriteriaDetails', err)
    });
  }

  getStatusBadgeClass(code: string): string 
  {
    const classes: { [key: string]: string } = 
    {
      'X': 'badge bg-danger',
      'O': 'badge bg-warning text-dark',
      'NA': 'badge bg-secondary',
      'X ONC': 'badge bg-success'
    };
    return classes[code] || 'badge bg-secondary';
  }

  onFilter(filters: any): void 
  {
    if (filters.reset) 
    {
      this.filteredCriteria = [...this.allCriteria];
      return;
    }
    
    this.filteredCriteria = this.allCriteria.filter(c => 
    {
      const matchDesc = !filters.description || 
        c.description.toLowerCase().includes(filters.description.toLowerCase());
      const matchType = !filters.typeCode || c.typeCode === filters.typeCode;
      const matchPoints = !filters.basePoints || c.basePoints === Number(filters.basePoints);
      
      return matchDesc && matchType && matchPoints;
    });
  }

  onAdd(): void 
  {
    this.loadingSearch = false;
    this.loadingReset = false;
    this.popupMode = 'create';
    this.currentCriterion = null;
    this.popupTitle = this.translate.instant('CriteresSection.AddCriterion');
    this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
    this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
    this.popupError = null;
    this.loading = false;
    this.showPopup = true;
  }

  onEdit(criterion: StarCriterionDetail): void 
  {
    this.loadingSearch = false;
    this.loadingReset = false;
    this.loadingSearch = false;
    this.loadingReset = false;
    this.popupMode = 'edit';
    this.currentCriterion = { ...criterion };
    this.popupTitle = this.translate.instant('CriteresSection.EditCriterion');
    this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
    this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
    this.popupError = null;
    this.loading = false;
    this.showPopup = true;
  }

  onDelete(criterion: StarCriterionDetail): void 
  {
    this.popupMode = 'delete';
    this.currentCriterion = criterion;
    this.popupTitle = this.translate.instant('CriteresSection.Delete');
    this.popupConfirmLabel = this.translate.instant('PopupSection.Validate');
    this.popupCancelLabel = this.translate.instant('PopupSection.Cancel');
    this.popupError = null;
    this.loading = false;
    this.showPopup = true;
  }

  onSavePopup(): void 
  {
    if (this.popupMode === 'delete') {
       if (!this.currentCriterion) return;
        this.loading = true;
        this.criteresBll.deleteCriterion$(this.currentCriterion.criterionId).subscribe({
          next: () => {
            this.loading = false;
            this.toast.show(
              this.translate.instant('CriteresSection.DeleteSuccess'),
              'success',
              5000
            );
            this.loadData();
            this.closePopup();
          },
          error: (err) => {
            this.loading = false;
            this.popupError = err.error?.message || 
              this.translate.instant('CommonSection.UnknownError');
          }
        });
      return;
    }

    if (this.formComponent.form.invalid) {
      this.formComponent.form.markAllAsTouched();
      return;
    }

    this.loading = true;
    const formValue = this.formComponent.getValue();
    
    const request: CreateCriterionRequest = {
      starCriterion: {
        description: formValue.description,
        basePoints: formValue.basePoints
      },
      starLevelCriterion: {
        starLevelId: this.starRating,
        typeCode: formValue.typeCode
      }
    };

    if (this.popupMode === 'edit') {
      if (!this.currentCriterion) return;
      if (this.formComponent.form.invalid) {
        this.formComponent.form.markAllAsTouched();
        return;
      }

      this.loading = true;
      const formValue = this.formComponent.getValue();

      const request: UpdateCriterionRequest = {
        criterionId: this.currentCriterion.criterionId,
        description: formValue.description,
        basePoints: formValue.basePoints,
        typeCode: formValue.typeCode,
        starLevelId: this.starRating
      };

      this.criteresBll.updateCriterion$(this.currentCriterion.criterionId, request).subscribe({
        next: () => {
          this.loading = false;
          this.toast.show(
            this.translate.instant('CriteresSection.EditSuccess'),
            'success',
            5000
          );
          this.loadData();
          this.closePopup();
        },
        error: (err) => {
          this.loading = false;
          this.popupError = err.error?.message || 
            this.translate.instant('CommonSection.UnknownError');
        }
      });
      return;
    } 

    else if (this.popupMode === 'create') {
      this.criteresBll.createCriterion$(request).subscribe({
      next: (response: { isSuccess: boolean; message?: string }) => {
        this.loading = false;
        if (response.isSuccess) {
          this.toast.show(
            this.translate.instant('CriteresSection.CreateSuccess'),
            'success',
            5000
          );
          this.loadData();
          this.closePopup();
        } else {
          this.popupError = response.message || 
            this.translate.instant('CommonSection.UnknownError');
        }
      },
      error: (err: HttpErrorResponse) => {
        this.loading = false;
        this.popupError = err.error?.message || 
          this.translate.instant('CommonSection.UnknownError');
      }
    });
    }
  }

  closePopup(): void {
    this.showPopup = false;
    this.currentCriterion = null;
    this.popupMode = null;
  }
}
