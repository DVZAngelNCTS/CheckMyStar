import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { CriteresBllService } from '../../../60_Bll/BackOffice/Criteres-bll.service';
import { StarCriteria } from '../../../20_Models/BackOffice/Criteres.model';
import { RatingContextService } from './Service/Rating-context.service';

@Component({
  selector: 'app-criteres-page',
  standalone: true,
  imports: [CommonModule, RouterModule, TranslationModule],
  templateUrl: './Criteres-page.component.html',
  styleUrls: ['./Criteres-page.component.css']
})
export class CriteresPageComponent implements OnInit {
  stars?: StarCriteria[] = [];

  constructor(private criteresBll: CriteresBllService, private router: Router, private ratingContext: RatingContextService) {
  }

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.criteresBll.getStarCriterias$().subscribe({
      next: data => this.stars = data.starCriterias,
      error: err => console.error('Erreur getStarCriteria', err)
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

  onManageCriteria(star: StarCriteria): void {
    this.ratingContext.rating = star.rating;
    this.router.navigate(['/backhome/criteres/management']);
  }
}
