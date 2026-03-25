import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { QuoteBllService } from '../../../../60_Bll/BackOffice/Quote-bll.service';
import { QuoteModel } from '../../../../20_Models/BackOffice/Quote.model';
import { QuoteDetailPreviewComponent } from './Quote-detail-preview.component';

@Component({
  selector: 'app-front-quote-detail-page',
  standalone: true,
  imports: [CommonModule, QuoteDetailPreviewComponent],
  template: `
    <div class="quote-detail-layout">
      @if (quote) {
        <app-quote-detail-preview [quote]="quote"></app-quote-detail-preview>
      }
    </div>
  `,
  styles: [`
    :host {
      flex: 1;
      display: flex;
      min-height: 0;
      overflow: hidden;
    }
    .quote-detail-layout {
      display: flex;
      flex: 1;
      min-height: 0;
      overflow: hidden;
    }
  `]
})
export class FrontQuoteDetailPageComponent implements OnInit {

  quote: QuoteModel | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private quoteBll: QuoteBllService
  ) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      this.router.navigate(['/fronthome/devis']);
      return;
    }

    this.quoteBll.getQuote$(id).subscribe({
      next: response => {
        const quote = response.quotes?.find(q => q.identifier === id) ?? response.quotes?.[0];
        if (!quote) {
          this.router.navigate(['/fronthome/devis']);
          return;
        }
        this.quote = quote;
      },
      error: err => console.error('Erreur chargement devis', err)
    });
  }
}
