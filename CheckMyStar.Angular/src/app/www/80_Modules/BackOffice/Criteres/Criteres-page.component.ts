// www/80_Modules/BackOffice/Criteres/Criteres-page.component.ts

import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { TranslationModule } from '../../../10_Common/Translation.module';
import { CriteresBllService } from '../../../60_Bll/BackOffice/Criteres-bll.service';
import { StarCriteria, StarCriteriaDetail, StarCriterionDetail } from '../../../20_Models/BackOffice/Criteres.model';

import { BreadcrumbNavService } from '../../../90_Services/Breadcrumb/BreadcrumbNav.service';

@Component({
  selector: 'app-criteres-page',
  standalone: true,
  imports: [CommonModule, RouterModule, TranslationModule],
  templateUrl: './Criteres-page.component.html',
  styleUrl: './Criteres-page.component.css'
})
export class CriteresPageComponent implements OnInit {

  stars: StarCriteria[] = [];
  starDetails: StarCriteriaDetail[] = [];

  showCards = true;
  selectedStar: StarCriteria | null = null;
  selectedStarDetails: StarCriterionDetail[] = [];

  constructor(
    private criteresBll: CriteresBllService,
    private breadcrumbNav: BreadcrumbNavService
  ) {}

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    if (this.breadcrumbNav.customBackAction) {
      this.breadcrumbNav.customBackAction = null;
    }
  }

  private loadData(): void {
    this.criteresBll.getStarCriteria().subscribe({
      next: data => this.stars = data,
      error: err => console.error('Erreur getStarCriteria', err)
    });

    this.criteresBll.getStarCriteriaDetails().subscribe({
      next: data => this.starDetails = data,
      error: err => console.error('Erreur getStarCriteriaDetails', err)
    });
  }

  getStatusBadgeClass(code: string): string {
  const classes: { [key: string]: string } = {
    'X': 'badge bg-danger',                // Critères obligatoires
    'O': 'badge bg-warning text-dark',     // À la carte
    'NA': 'badge bg-secondary',            // Non applicable
    'X ONC': 'badge bg-success'            // Obligatoire non compensable
  };

    return classes[code] || 'badge bg-secondary';
  }


  onManageCriteria(star: StarCriteria): void {
    this.showCards = false;
    this.selectedStar = star;

    const detailsForStar = this.starDetails.find(d => d.rating === star.rating);
    this.selectedStarDetails = detailsForStar ? detailsForStar.criteria : [];

    this.breadcrumbNav.customBackAction = () => this.onBackToCards();
  }

  onBackToCards(): void {
    this.showCards = true;
    this.selectedStar = null;
    this.selectedStarDetails = [];

    this.breadcrumbNav.customBackAction = null;
  }

  onEditCriterion(c: StarCriterionDetail): void {
    console.log('Modifier critère', c);
  }

  onDeleteCriterion(c: StarCriterionDetail): void {
    console.log('Supprimer critère', c);
  }

}
