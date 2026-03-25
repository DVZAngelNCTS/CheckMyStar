import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { TableComponent } from '../../Components/Table/Table.component';
import { TableColumn } from '../../Components/Table/Models/TableColumn.model';
import { QuoteBllService } from '../../../60_Bll/BackOffice/Quote-bll.service';
import { QuoteModel } from '../../../20_Models/BackOffice/Quote.model';
import { ToastService } from '../../../90_Services/Toast/Toast.service';
import { FrontQuoteFilterComponent } from './Filter/Quote-filter.component';
import { QuoteFilter } from '../../../30_Filters/BackOffice/Quote.filter';
import { Router } from '@angular/router';
import { PopupComponent } from '../../Components/Popup/Popup.component';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-front-quotes-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, TableComponent, FormsModule, ReactiveFormsModule, FrontQuoteFilterComponent, PopupComponent],
  templateUrl: './Quotes-page.component.html'
})
export class FrontQuotesPageComponent implements OnInit {

  loading = false;
  loadingSearch = false;
  loadingReset = false;
  quotes: QuoteModel[] = [];
  private allQuotes: QuoteModel[] = [];

  popupVisible = false;
  popupMode: 'delete' | 'enabled' | null = null;
  popupTitle = '';
  popupError: string | null = null;
  selectedQuote: QuoteModel | null = null;

  columns = [
    { icon: 'bi bi-tag', field: 'reference', header: 'FrontQuoteListSection.Reference', sortable: true, filterable: true, width: '15%',
      pipe: (_, row) => {
        if (row.reference) return row.reference;
        const year = row.createdDate ? new Date(row.createdDate).getFullYear() : new Date().getFullYear();
        const counter = String(row.identifier).padStart(4, '0');
        return `DEV-${year}-${counter}`;
      }
    },
    { icon: 'bi bi-calendar-event', field: 'createdDate', header: 'FrontQuoteListSection.CreatedDate', sortable: true, filterable: true, width: '12%',
      pipe: (_, row) => {
        if (!row.createdDate) return '';
        return new Date(row.createdDate).toLocaleDateString('fr-FR');
      }
    },
    { icon: 'bi bi-person', field: 'clientName', header: 'FrontQuoteListSection.ClientName', sortable: true, filterable: true, width: '20%',
      pipe: (_, row) => row.clientName ?? [row.clientUser?.firstName, row.clientUser?.lastName].filter(Boolean).join(' ')
    },
    { icon: 'bi bi-currency-euro', field: 'totalAmountTTC', header: 'FrontQuoteListSection.TotalAmountTTC', sortable: true, filterable: true, width: '13%',
      pipe: (_, row) => {
        if (row.totalAmountTTC == null) return '';
        return row.totalAmountTTC.toFixed(2) + ' €';
      }
    },
    { icon: 'bi bi-calendar-check', field: 'validityDate', header: 'FrontQuoteListSection.ValidityDate', sortable: true, filterable: true, width: '12%',
      pipe: (_, row) => {
        if (!row.validityDate) return '';
        return new Date(row.validityDate).toLocaleDateString('fr-FR');
      }
    },
    { icon: 'bi bi-info-circle', field: 'quoteStatusIdentifier', header: 'FrontQuoteListSection.QuoteStatus', sortable: true, filterable: false, width: '13%',
      pipe: (_, row) => {
        const map: Record<number, string> = { 1: 'Brouillon', 2: 'Envoyé', 3: 'Accepté', 4: 'Refusé' };
        return map[row.quoteStatusIdentifier ?? 0] ?? '';
      }
    }
  ] as TableColumn<QuoteModel>[];

  constructor(
    private quoteBll: QuoteBllService,
    private toast: ToastService,
    private router: Router,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    this.loadQuotes();
  }

  loadQuotes(): void {
    this.quoteBll.getQuotes$().subscribe({
      next: response => {
        this.allQuotes = response.quotes ?? [];
        this.quotes = this.allQuotes;
      },
      error: err => console.error(err)
    });
  }

  onFilter(filter: QuoteFilter): void {
    if (filter.reset) {
      this.loadingReset = true;
    } else {
      this.loadingSearch = true;
    }

    const isAccepted = filter.isAccepted != null ? filter.isAccepted as boolean : undefined;

    this.quoteBll.getQuotes$(filter.reference, isAccepted).subscribe({
      next: response => {
        if (filter.reset) this.loadingReset = false;
        else this.loadingSearch = false;
        this.allQuotes = response.quotes ?? [];
        this.quotes = this.applyClientFilters(this.allQuotes, filter);
      },
      error: err => {
        if (filter.reset) this.loadingReset = false;
        else this.loadingSearch = false;
        console.error(err);
      }
    });
  }

  private applyClientFilters(quotes: QuoteModel[], filter: QuoteFilter): QuoteModel[] {
    let result = quotes;

    const normalizedReference = (filter.reference ?? '').trim().toLowerCase();
    if (normalizedReference) {
      result = result.filter(q => this.getQuoteReference(q).toLowerCase().includes(normalizedReference));
    }

    if (filter.isAccepted != null) {
      result = result.filter(q => (q.quoteStatusIdentifier === 3) === filter.isAccepted);
    }

    if (filter.createdDateFrom) {
      const from = new Date(filter.createdDateFrom);
      result = result.filter(q => q.createdDate ? new Date(q.createdDate) >= from : true);
    }
    if (filter.createdDateTo) {
      const to = new Date(filter.createdDateTo);
      result = result.filter(q => q.createdDate ? new Date(q.createdDate) <= to : true);
    }
    if (filter.totalAmountTTCMin != null) {
      result = result.filter(q => (q.totalAmountTTC ?? 0) >= filter.totalAmountTTCMin!);
    }
    if (filter.totalAmountTTCMax != null) {
      result = result.filter(q => (q.totalAmountTTC ?? 0) <= filter.totalAmountTTCMax!);
    }
    if (filter.validityDateFrom) {
      const from = new Date(filter.validityDateFrom);
      result = result.filter(q => q.validityDate ? new Date(q.validityDate) >= from : true);
    }
    if (filter.validityDateTo) {
      const to = new Date(filter.validityDateTo);
      result = result.filter(q => q.validityDate ? new Date(q.validityDate) <= to : true);
    }

    return result;
  }

  private getQuoteReference(quote: QuoteModel): string {
    if (quote.reference) return quote.reference;

    const year = quote.createdDate ? new Date(quote.createdDate).getFullYear() : new Date().getFullYear();
    const counter = String(quote.identifier).padStart(4, '0');
    return `DEV-${year}-${counter}`;
  }

  onDetail(quote: QuoteModel): void {
    this.router.navigate(['/fronthome/devis', quote.identifier, 'view']);
  }

  onUpdate(quote: QuoteModel): void {
    const editableStatuses = [1, 4]; // 1 = Brouillon, 4 = Refusé
    if (!editableStatuses.includes(quote.quoteStatusIdentifier ?? 0)) {
      this.toast.show(this.translate.instant('FrontQuoteListSection.EditNotAllowed'), 'error', 5000);
      return;
    }
    this.router.navigate(['/fronthome/devis', quote.identifier, 'edit']);
  }

  onDelete(quote: QuoteModel): void {
    this.loading = false;
    this.popupError = null;
    this.selectedQuote = quote;
    this.popupMode = 'delete';
    this.popupTitle = this.translate.instant('FrontQuoteListSection.Delete');
    this.popupVisible = true;
  }

  toggleEnabled(quote: QuoteModel): void {
    this.loading = false;
    this.popupError = null;
    this.selectedQuote = quote;
    this.popupMode = 'enabled';
    this.popupTitle = this.translate.instant(quote.isActive ? 'FrontQuoteListSection.Disable' : 'FrontQuoteListSection.Enable');
    this.popupVisible = true;
  }

  onPopupConfirm(): void {
    if (this.popupMode === 'delete') {
      this.onDeleteConfirmed();
    } else if (this.popupMode === 'enabled') {
      this.onEnabledConfirmed();
    }
  }

  onPopupCancel(): void {
    this.popupVisible = false;
    this.selectedQuote = null;
    this.popupMode = null;
  }

  private onDeleteConfirmed(): void {
    if (!this.selectedQuote) return;
    this.loading = true;

    this.quoteBll.deleteQuote$(this.selectedQuote.identifier).subscribe({
      next: response => {
        this.loading = false;
        if (!response.isSuccess) {
          this.popupError = response.message;
          return;
        }
        this.popupError = null;
        this.popupVisible = false;
        this.toast.show(response.message, 'success', 5000);
        this.loadQuotes();
      },
      error: err => {
        this.loading = false;
        this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  private onEnabledConfirmed(): void {
    if (!this.selectedQuote) return;
    this.loading = true;

    const updated: QuoteModel = { ...this.selectedQuote, isActive: !this.selectedQuote.isActive };

    this.quoteBll.enabledQuote$(updated).subscribe({
      next: response => {
        this.loading = false;
        if (!response.isSuccess) {
          this.popupError = response.message;
          return;
        }
        this.popupError = null;
        this.popupVisible = false;
        this.toast.show(response.message, 'success', 5000);
        this.loadQuotes();
      },
      error: err => {
        this.loading = false;
        this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  onCreate(): void {
    this.router.navigate(['/fronthome/devis', 0]);
  }
}
