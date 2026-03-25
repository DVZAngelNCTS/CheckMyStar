import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { QuoteModel } from '../../../../20_Models/BackOffice/Quote.model';
import { QuoteBllService } from '../../../../60_Bll/BackOffice/Quote-bll.service';
import { ToastService } from '../../../../90_Services/Toast/Toast.service';

@Component({
  selector: 'app-quote-detail-preview',
  standalone: true,
  imports: [CommonModule, TranslationModule],
  templateUrl: './Quote-detail-preview.component.html',
  styleUrls: ['./Quote-detail-preview.component.css']
})
export class QuoteDetailPreviewComponent {

  @Input() quote!: QuoteModel;

  constructor(
    private quoteBll: QuoteBllService,
    private toast: ToastService
  ) {}

  get companyName(): string {
    return this.quote.companySociety?.name ?? '';
  }

  get companyAddress(): string {
    const a = this.quote.companyAddress;
    if (!a) return '';
    return [a.number, a.addressLine, a.zipCode, a.city, a.country?.name].filter(Boolean).join(', ');
  }

  get quoteNumber(): string {
    if (this.quote.reference) return this.quote.reference;
    const year = this.quote.createdDate ? new Date(this.quote.createdDate).getFullYear() : new Date().getFullYear();
    return `DEV-${year}-${String(this.quote.identifier).padStart(4, '0')}`;
  }

  get quoteDate(): string {
    return this.formatDate(this.quote.createdDate);
  }

  get validityDate(): string {
    return this.formatDate(this.quote.validityDate);
  }

  get clientName(): string {
    const u = this.quote.clientUser;
    return u ? `${u.lastName} ${u.firstName}`.trim() : '';
  }

  get clientAddress(): string {
    const a = this.quote.clientAddress;
    if (!a) return '';
    return [a.number, a.addressLine, a.zipCode, a.city, a.country?.name].filter(Boolean).join(', ');
  }

  get lines() {
    return this.quote.quoteLines ?? [];
  }

  get vatRate(): number {
    return this.quote.quoteLines?.[0]?.vatRate ?? 20;
  }

  get subTotalHT(): number {
    return this.lines.reduce((sum, l) => sum + l.quantity * l.unitPriceHT, 0);
  }

  get totalHT(): number {
    return this.subTotalHT;
  }

  get vatAmount(): number {
    return this.totalHT * (this.vatRate / 100);
  }

  get totalTTC(): number {
    return this.totalHT * (1 + this.vatRate / 100);
  }

  get executionDate(): string {
    return this.formatDate(this.quote.executionDate);
  }

  get statusLabel(): string {
    const map: Record<number, string> = { 1: 'Brouillon', 2: 'Envoyé', 3: 'Accepté', 4: 'Refusé' };
    return map[this.quote.quoteStatusIdentifier ?? 1] ?? '';
  }

  get statusClass(): string[] {
    const map: Record<number, string[]> = {
      1: ['bg-secondary', 'text-white'],
      2: ['bg-primary',   'text-white'],
      3: ['bg-success',   'text-white'],
      4: ['bg-danger',    'text-white']
    };
    return map[this.quote.quoteStatusIdentifier ?? 1] ?? [];
  }

  formatDate(value: string | null | undefined): string {
    if (!value) return '—';
    const d = new Date(value);
    if (isNaN(d.getTime())) return value;
    return d.toLocaleDateString('fr-FR');
  }

  private downloadPdf(blob: Blob, identifier: number): void {
    const fileName = `devis-${String(identifier).padStart(4, '0')}.pdf`;
    const url = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = url;
    link.download = fileName;
    link.click();
    URL.revokeObjectURL(url);
  }

  generatePdf(): void {
    const identifier = this.quote?.identifier ?? 0;
    if (!identifier) {
      this.toast.show('Le numéro du devis est invalide.', 'error', 5000);
      return;
    }

    this.quoteBll.generatePdf$(identifier).subscribe({
      next: (pdfBlob) => this.downloadPdf(pdfBlob, identifier),
      error: (err: unknown) => {
        console.error('Erreur génération PDF', err);
        this.toast.show('La génération du PDF a échoué.', 'error', 5000);
      }
    });
  }
}
