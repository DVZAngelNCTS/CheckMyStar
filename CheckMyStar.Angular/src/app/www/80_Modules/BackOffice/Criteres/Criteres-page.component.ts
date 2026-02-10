import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
  selector: 'app-criteres-page',
  standalone: true,
  imports: [CommonModule, RouterModule, TranslationModule],
  templateUrl: './Criteres-page.component.html'
})
export class CriteresPageComponent {
  stars = [
    { rating: 1, label: '1 Étoile', description: 'Critères 1 étoile', lastUpdate: new Date(), statuses: [ { code: 'X', label: 'Obligatoire', count: 12 }, { code: 'O', label: 'À la carte', count: 6 }, { code: 'NA', label: 'Non applicable', count: 52 }, { code: 'X ONC', label: 'Obligatoire non compensable', count: 30 } ] },
    { rating: 2, label: '2 Étoiles', description: 'Critères 2 étoiles', lastUpdate: new Date(), statuses: [ { code: 'X', label: 'Obligatoire', count: 25 }, { code: 'O', label: 'À la carte', count: 18 }, { code: 'NA', label: 'Non applicable', count: 45 }, { code: 'X ONC', label: 'Obligatoire non compensable', count: 45 } ] },
    { rating: 3, label: '3 Étoiles', description: 'Critères 3 étoiles', lastUpdate: new Date(), statuses: [ { code: 'X', label: 'Obligatoire', count: 35 }, { code: 'O', label: 'À la carte', count: 25 }, { code: 'NA', label: 'Non applicable', count: 40 }, { code: 'X ONC', label: 'Obligatoire non compensable', count: 33 } ] },
    { rating: 4, label: '4 Étoiles', description: 'Critères 4 étoiles', lastUpdate: new Date(), statuses: [ { code: 'X', label: 'Obligatoire', count: 42 }, { code: 'O', label: 'À la carte', count: 30 }, { code: 'NA', label: 'Non applicable', count: 35 }, { code: 'X ONC', label: 'Obligatoire non compensable', count: 26 } ] },
    { rating: 5, label: '5 Étoiles', description: 'Critères 5 étoiles', lastUpdate: new Date(), statuses: [ { code: 'X', label: 'Obligatoire', count: 50 }, { code: 'O', label: 'À la carte', count: 35 }, { code: 'NA', label: 'Non applicable', count: 30 }, { code: 'X ONC', label: 'Obligatoire non compensable', count: 18 } ] }
  ];

  getStatusBadgeClass(code: string): string {
    const classes: { [key: string]: string } = {
      'X': 'bg-danger',
      'O': 'bg-warning',
      'NA': 'bg-secondary',
      'X ONC': 'bg-info'
    };
    return classes[code] || 'bg-secondary';
  }
}
