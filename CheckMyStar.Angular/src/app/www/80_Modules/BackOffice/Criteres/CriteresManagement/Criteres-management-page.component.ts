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

@Component({
  selector: 'app-criteres-management-page',
  standalone: true,
  imports: [
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
export class CriteresManagementPageComponent implements OnInit {
  @ViewChild(CriteresFormComponent) formComponent!: CriteresFormComponent;
  
  selectedStar: StarCriteria | null = null;
  allCriteria: StarCriterionDetail[] = [];
  filteredCriteria: StarCriterionDetail[] = [];
  starRating: number = 0;
  
  loadingSearch = false;
  loadingReset = false;
  
  columns: TableColumn<StarCriterionDetail>[] = [
    { field: 'criterionId', header: 'Id', icon: '', sortable: true, width: '80px' },
    { field: 'description', header: 'Description', icon: '', sortable: true },
    { field: 'basePoints', header: 'Points', icon: '', sortable: true, width: '100px' },
    { field: 'typeCode', header: 'Type', icon: '', sortable: true, width: '120px' }
  ];

  // Popup state
  showPopup = false;
  popupTitle = '';
  popupWidth = '600px';
  popupHeight = 'auto';
  currentCriterion: StarCriterionDetail | null = null;
  isEdit = false;

  constructor(private criteresBll: CriteresBllService, private route: ActivatedRoute, private router: Router, private ratingContext: RatingContextService) {
  }

  ngOnInit(): void {
    this.starRating = this.ratingContext.rating || 0;
    if (!this.starRating) {
      this.router.navigate(['/backhome/criteres']);
      return;
    }
    this.loadData();
  }

  private loadData(): void {
    this.criteresBll.getStarCriterias$().subscribe({
      next: stars => {
        this.selectedStar = stars.starCriterias?.find(s => s.rating === this.starRating) || null;
      },
      error: err => console.error('Erreur getStarCriteria', err)
    });

    this.criteresBll.getStarCriteriaDetails$().subscribe({
      next: details => {
        const detailsForStar = details.starCriterias?.find(d => d.rating === this.starRating);
        this.allCriteria = detailsForStar ? detailsForStar.criteria : [];
        this.filteredCriteria = [...this.allCriteria];
      },
      error: err => console.error('Erreur getStarCriteriaDetails', err)
    });
  }

  getStatusBadgeClass(code: string): string {
    const classes: { [key: string]: string } = {
      'X': 'badge bg-danger',
      'O': 'badge bg-warning text-dark',
      'NA': 'badge bg-secondary',
      'X ONC': 'badge bg-success'
    };
    return classes[code] || 'badge bg-secondary';
  }

  onFilter(filters: any): void {
    if (filters.reset) {
      this.filteredCriteria = [...this.allCriteria];
      return;
    }
    
    this.filteredCriteria = this.allCriteria.filter(c => {
      const matchDesc = !filters.description || 
        c.description.toLowerCase().includes(filters.description.toLowerCase());
      const matchType = !filters.typeCode || c.typeCode === filters.typeCode;
      const matchPoints = !filters.basePoints || c.basePoints === Number(filters.basePoints);
      
      return matchDesc && matchType && matchPoints;
    });
  }

  onAdd(): void {
    this.isEdit = false;
    this.currentCriterion = null;
    this.popupTitle = 'CriteresSection.AddCriterion';
    this.showPopup = true;
  }

  onEdit(criterion: StarCriterionDetail): void {
    this.isEdit = true;
    this.currentCriterion = { ...criterion };
    this.popupTitle = 'CriteresSection.EditCriterion';
    this.showPopup = true;
  }

  onDelete(criterion: StarCriterionDetail): void {
    if (confirm(`Supprimer le critère "${criterion.description}" ?`)) {
      // Appel au BLL pour supprimer
      console.log('Supprimer', criterion);
    }
  }

  onSavePopup(): void {
    if (this.formComponent.form.valid) {
      const value = this.formComponent.getValue();
      if (this.isEdit) {
        console.log('Modifier', value);
      } else {
        console.log('Créer', value);
      }
      this.closePopup();
      // Recharger les données après sauvegarde
    }
  }

  closePopup(): void {
    this.showPopup = false;
    this.currentCriterion = null;
  }
}
