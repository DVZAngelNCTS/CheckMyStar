import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, FormArray } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { UserModel } from '../../../../20_Models/Common/User.model';
import { SocietyModel } from '../../../../20_Models/BackOffice/Society.model';
import { QuoteModel, QuoteLineModel } from '../../../../20_Models/BackOffice/Quote.model';
import { QuoteBllService } from '../../../../60_Bll/BackOffice/Quote-bll.service';
import { ToastService } from '../../../../90_Services/Toast/Toast.service';
import { Environment } from '../../../../../../Environment/environment';

@Component({
  selector: 'app-quote-create-preview',
  standalone: true,
  imports: [CommonModule, TranslationModule],
  templateUrl: './Quote-create-preview.component.html',
  styleUrls: ['./Quote-create-preview.component.css']
})
export class QuoteCreatePreviewComponent {

  @Input() quoteForm!: FormGroup;
  @Input() currentUser: UserModel | undefined;
  @Input() clients: UserModel[] = [];
  @Input() currentSociety: SocietyModel | undefined;
  readonly apiUrl = Environment.ApiUrl;

  constructor(
    private quoteBll: QuoteBllService,
    private toast: ToastService
  ) {}

  get services(): FormArray {
    return this.quoteForm.get('services') as FormArray;
  }

  get subTotalHT(): number {
    return this.services.controls.reduce((sum, ctrl) => {
      const q = +ctrl.get('quantity')?.value || 0;
      const p = +ctrl.get('unitPrice')?.value || 0;
      return sum + q * p;
    }, 0);
  }

  get totalHT(): number {
    return this.subTotalHT;
  }

  get vatAmount(): number {
    const rate = +this.quoteForm.get('vatRate')?.value || 0;
    return this.totalHT * (rate / 100);
  }

  get totalTTC(): number {
    const rate = +this.quoteForm.get('vatRate')?.value || 0;
    return this.totalHT * (1 + rate / 100);
  }

  get statusLabel(): string {
    const map: Record<string, string> = {
      draft: 'Brouillon', sent: 'Envoyé', accepted: 'Accepté', refused: 'Refusé'
    };
    return map[this.quoteForm.get('status')?.value] || '';
  }

  get statusClass(): string[] {
    const map: Record<string, string[]> = {
      draft:    ['bg-secondary', 'text-white'],
      sent:     ['bg-primary',   'text-white'],
      accepted: ['bg-success',   'text-white'],
      refused:  ['bg-danger',    'text-white']
    };
    return map[this.quoteForm.get('status')?.value] || [];
  }

  get companyLogoUrl(): string | null {
    const rawLogoPath = this.quoteForm.get('companyLogoPath')?.value as string | null | undefined;
    return this.resolveLogoUrl(rawLogoPath ?? '');
  }

  formatDate(value: string | null | undefined): string {
    if (!value) return '—';
    const parts = value.split('-');
    if (parts.length !== 3) return value;
    return `${parts[2]}/${parts[1]}/${parts[0]}`;
  }

  private resolveLogoUrl(path: string): string | null {
    const trimmedPath = path?.trim();

    if (!trimmedPath) {
      return null;
    }

    if (trimmedPath.startsWith('http://') || trimmedPath.startsWith('https://') || trimmedPath.startsWith('blob:')) {
      return trimmedPath;
    }

    const cleanApiUrl = this.apiUrl.replace(/\/$/, '');
    const cleanPath = trimmedPath.replace(/^\//, '');

    return `${cleanApiUrl}/${cleanPath}`;
  }

  private getIdentifierFromQuoteNumber(): number {
    const quoteNumber = this.quoteForm.get('quoteNumber')?.value as string | undefined;
    if (!quoteNumber) return 0;

    const match = quoteNumber.match(/(\d+)$/);
    return match ? Number(match[1]) : 0;
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
    const identifier = this.getIdentifierFromQuoteNumber();
    if (!identifier) {
      this.toast.show('Le numéro de devis est invalide.', 'error', 5000);
      return;
    }

    const selectedClientIdentifier = +this.quoteForm.get('clientIdentifier')?.value || 0;
    const clientName = this.quoteForm.get('clientName')?.value ?? '';
    const client = this.clients.find(c => c.identifier === selectedClientIdentifier)
      ?? this.clients.find(c => `${c.lastName} ${c.firstName}` === clientName);

    const now = new Date().toISOString();
    const validityRaw = this.quoteForm.get('validityPeriod')?.value;
    const executionRaw = this.quoteForm.get('executionDate')?.value;

    const vatRate = +this.quoteForm.get('vatRate')?.value || 0;

    const quoteLines: QuoteLineModel[] = this.services.controls.map(ctrl => ({
      identifier: 0,
      quoteIdentifier: 0,
      description: ctrl.get('description')?.value ?? '',
      quantity: +ctrl.get('quantity')?.value || 0,
      unit: ctrl.get('unit')?.value ?? '',
      unitPriceHT: +ctrl.get('unitPrice')?.value || 0,
      vatRate: vatRate,
      createdDate: now,
      updatedDate: now,
    }));

    const quote: QuoteModel = {
      identifier: identifier,
      clientUserIdentifier: client?.identifier ?? 0,
      clientAddressIdentifier: client?.address?.identifier ?? 0,
      inspectorIdentifier: this.currentUser?.identifier ?? 0,
      companySocietyIdentifier: this.currentUser?.societyIdentifier ?? 0,
      companyAddressIdentifier: this.currentSociety?.address?.identifier ?? 0,
      companyLogoPath: this.quoteForm.get('companyLogoPath')?.value ?? '',
      companyEmail: this.quoteForm.get('companyEmail')?.value ?? '',
      companyPhone: this.quoteForm.get('companyPhone')?.value ?? '',
      companySiretCode: this.quoteForm.get('companySiretCode')?.value ?? '',
      companyVatNumber: this.quoteForm.get('companyVatNumber')?.value ?? '',
      companyLegalInformation: this.quoteForm.get('companyLegalInformation')?.value ?? '',
      totalAmountHT: this.totalHT,
      totalAmountTTC: this.totalTTC,
      validityDate: validityRaw ? new Date(validityRaw).toISOString() : now,
      executionDate: executionRaw ? new Date(executionRaw).toISOString() : undefined,
      quoteStatusIdentifier: 1,
      createdDate: now,
      updatedDate: now,
      isEditable: true,
      quoteLines: quoteLines,
    };

    this.quoteBll.addQuote$(quote).subscribe({
      next: (res) => {
        if (res.isSuccess) {
          this.quoteBll.generatePdf$(identifier).subscribe({
            next: (pdfBlob) => {
              this.downloadPdf(pdfBlob, identifier);
              this.quoteForm.patchValue({ status: 'sent' });
              this.toast.show(res.message, 'success', 5000);
            },
            error: (err: unknown) => {
              console.error('Erreur génération PDF', err);
              this.toast.show('Le devis a été créé mais la génération du PDF a échoué.', 'error', 5000);
            }
          });
        } else {
          this.toast.show(res.message, 'error', 5000);
        }
      },
      error: (err: unknown) => {
        console.error('Erreur création devis', err);
        this.toast.show('Une erreur est survenue lors de la création du devis.', 'error', 5000);
      }
    });
  }
}
