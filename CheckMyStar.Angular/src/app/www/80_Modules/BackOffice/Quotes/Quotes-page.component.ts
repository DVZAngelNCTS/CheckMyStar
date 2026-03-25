import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { QuoteFilterComponent } from './Filter/Quote-filter.component';
import { TableComponent } from '../../Components/Table/Table.component';
import { TableColumn } from '../../Components/Table/Models/TableColumn.model';
import { QuoteModel } from '../../../20_Models/BackOffice/Quote.model';
import { QuoteFilter } from '../../../30_Filters/BackOffice/Quote.filter';
import { QuoteBllService } from '../../../60_Bll/BackOffice/Quote-bll.service';

@Component({
  selector: 'app-quotes-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, QuoteFilterComponent, TableComponent],
  templateUrl: './Quotes-page.component.html'
})
export class QuotesPageComponent implements OnInit {
  loadingSearch = false;
  loadingReset = false;
  quotes: QuoteModel[] = [];

  columns = [
    {
      icon: 'bi bi-tag',
      field: 'reference',
      header: 'FrontQuoteListSection.Reference',
      sortable: true,
      filterable: true,
      width: '18%',
      pipe: (_: unknown, row: QuoteModel) => this.getDisplayReference(row)
    },
    {
      icon: 'bi bi-calendar-event',
      field: 'createdDate',
      header: 'FrontQuoteListSection.CreatedDate',
      sortable: true,
      filterable: true,
      width: '16%',
      pipe: (_: unknown, row: QuoteModel) => this.toFrDate(row.createdDate)
    },
    {
      icon: 'bi bi-person',
      field: 'clientName',
      header: 'FrontQuoteListSection.ClientName',
      sortable: true,
      filterable: true,
      width: '24%',
      pipe: (_: unknown, row: QuoteModel) => row.clientName ?? [row.clientUser?.firstName, row.clientUser?.lastName].filter(Boolean).join(' ')
    },
    {
      icon: 'bi bi-currency-euro',
      field: 'totalAmountTTC',
      header: 'FrontQuoteListSection.TotalAmountTTC',
      sortable: true,
      filterable: true,
      width: '16%',
      pipe: (_: unknown, row: QuoteModel) => row.totalAmountTTC != null ? `${row.totalAmountTTC.toFixed(2)} €` : ''
    },
    {
      icon: 'bi bi-calendar-check',
      field: 'validityDate',
      header: 'FrontQuoteListSection.ValidityDate',
      sortable: true,
      filterable: true,
      width: '14%',
      pipe: (_: unknown, row: QuoteModel) => this.toFrDate(row.validityDate)
    },
    {
      icon: 'bi bi-info-circle',
      field: 'quoteStatusIdentifier',
      header: 'FrontQuoteListSection.QuoteStatus',
      sortable: true,
      filterable: false,
      width: '12%',
      pipe: (_: unknown, row: QuoteModel) => this.getQuoteStatusLabel(row.quoteStatusIdentifier)
    }
  ] as TableColumn<QuoteModel>[];

  constructor(private quoteBll: QuoteBllService) {}

  ngOnInit(): void {
    this.loadQuotes();
  }

  loadQuotes(filter?: QuoteFilter): void {
    const appliedFilter = filter?.reset ? undefined : filter;
    const isAccepted = appliedFilter?.isAccepted == null ? undefined : appliedFilter.isAccepted;

    this.quoteBll.getQuotes$(appliedFilter?.reference, isAccepted).subscribe({
      next: response => {
        this.loadingSearch = false;
        this.loadingReset = false;
        this.quotes = response.quotes ?? [];
      },
      error: err => {
        this.loadingSearch = false;
        this.loadingReset = false;
        console.error(err);
      }
    });
  }

  onFilter(filter: QuoteFilter): void {
    if (filter.reset) {
      this.loadingReset = true;
    } else {
      this.loadingSearch = true;
    }

    this.loadQuotes(filter);
  }

  private getDisplayReference(quote: QuoteModel): string {
    if (quote.reference) {
      return quote.reference;
    }

    const year = quote.createdDate ? new Date(quote.createdDate).getFullYear() : new Date().getFullYear();
    const counter = String(quote.identifier).padStart(4, '0');
    return `DEV-${year}-${counter}`;
  }

  private toFrDate(value?: string): string {
    if (!value) {
      return '';
    }

    const date = new Date(value);
    return Number.isNaN(date.getTime()) ? '' : date.toLocaleDateString('fr-FR');
  }

  private getQuoteStatusLabel(status?: number): string {
    const labels: Record<number, string> = {
      1: 'Brouillon',
      2: 'Envoyé',
      3: 'Accepté',
      4: 'Refusé'
    };

    return labels[status ?? 0] ?? '';
  }
}
