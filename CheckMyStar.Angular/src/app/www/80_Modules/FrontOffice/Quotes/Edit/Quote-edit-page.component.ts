import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { QuoteBllService } from '../../../../60_Bll/BackOffice/Quote-bll.service';
import { UserBllService } from '../../../../60_Bll/BackOffice/User-bll.service';
import { SocietyBllService } from '../../../../60_Bll/BackOffice/Society-bll.service';
import { SocietyResponse } from '../../../../50_Responses/BackOffice/Society.response';
import { SocietyModel } from '../../../../20_Models/BackOffice/Society.model';
import { UserModel } from '../../../../20_Models/Common/User.model';
import { QuoteModel } from '../../../../20_Models/BackOffice/Quote.model';
import { EnumRole } from '../../../../10_Common/Enumerations/EnumRole';
import { AuthenticateService } from '../../../../90_Services/Authenticate/Authenticate.service';
import { QuoteEditFormComponent } from './Quote-edit-form.component';
import { QuoteEditPreviewComponent } from './Quote-edit-preview.component';

@Component({
  selector: 'app-front-quote-edit-page',
  standalone: true,
  imports: [CommonModule, QuoteEditFormComponent, QuoteEditPreviewComponent],
  template: `
    <div class="quotes-layout">
      @if (quoteForm && quote) {
        <app-quote-edit-form
          [quoteForm]="quoteForm"
          [clients]="clients"
          [selectedClientIdentifier]="selectedClientIdentifier"
          (clientSelected)="onClientSelected($event)">
        </app-quote-edit-form>
        <app-quote-edit-preview [quoteForm]="quoteForm" [quote]="quote" [currentUser]="currentUser" [clients]="clients" [currentSociety]="currentSociety"></app-quote-edit-preview>
      }
    </div>
  `,
  styleUrls: ['./Quote-edit-page.component.css']
})
export class FrontQuoteEditPageComponent implements OnInit {

  quoteForm: FormGroup | null = null;
  quote: QuoteModel | null = null;
  clients: UserModel[] = [];
  selectedClientIdentifier: number | null = null;
  currentUser: UserModel | undefined;
  currentSociety: SocietyModel | undefined;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private quoteBll: QuoteBllService,
    private userBll: UserBllService,
    private societyBll: SocietyBllService,
    private authenticateService: AuthenticateService
  ) {}

  private createServiceGroup(line?: { identifier?: number; description?: string; quantity?: number; unit?: string; unitPriceHT?: number }): FormGroup {
    return this.fb.group({
      identifier:  [line?.identifier ?? 0],
      description: [line?.description ?? '', Validators.required],
      quantity:    [line?.quantity ?? 1, [Validators.required, Validators.min(0)]],
      unit:        [line?.unit ?? ''],
      unitPrice:   [line?.unitPriceHT ?? 0, [Validators.required, Validators.min(0)]]
    });
  }

  private buildForm(quote: QuoteModel): FormGroup {
    const statusMap: Record<number, string> = { 1: 'draft', 2: 'sent', 3: 'accepted', 4: 'refused' };
    const status = statusMap[quote.quoteStatusIdentifier ?? 1] ?? 'draft';

    const servicesArray = this.fb.array(
      (quote.quoteLines && quote.quoteLines.length > 0)
        ? quote.quoteLines.map(line => this.createServiceGroup(line))
        : [this.createServiceGroup()]
    );

    const clientAddress = quote.clientAddress
      ? [quote.clientAddress.number, quote.clientAddress.addressLine, quote.clientAddress.zipCode, quote.clientAddress.city, quote.clientAddress.country?.name].filter(Boolean).join(', ')
      : '';

    const companyAddress = quote.companyAddress
      ? [quote.companyAddress.number, quote.companyAddress.addressLine, quote.companyAddress.zipCode, quote.companyAddress.city, quote.companyAddress.country?.name].filter(Boolean).join(', ')
      : '';

    const clientName = quote.clientName
      ?? (quote.clientUser ? `${quote.clientUser.lastName} ${quote.clientUser.firstName}` : '');

    const year = quote.createdDate ? new Date(quote.createdDate).getFullYear() : new Date().getFullYear();
    const counter = String(quote.identifier).padStart(4, '0');
    const quoteNumber = quote.reference ?? `DEV-${year}-${counter}`;

    return this.fb.group({
      quoteNumber:    [quoteNumber],
      quoteDate:      [quote.createdDate ? quote.createdDate.split('T')[0] : new Date().toISOString().split('T')[0], Validators.required],
      clientIdentifier: [quote.clientUserIdentifier ?? null, Validators.required],
      clientName:     [clientName, Validators.required],
      clientAddress:  [clientAddress],
      companyName:    [quote.companySociety?.name ?? '', Validators.required],
      companyAddress: [companyAddress],
      companyLogoPath: [quote.companyLogoPath ?? quote.companySociety?.logoPath ?? ''],
      companyEmail: [quote.companyEmail ?? quote.companySociety?.email ?? ''],
      companyPhone: [quote.companyPhone ?? quote.companySociety?.phone ?? ''],
      companySiretCode: [quote.companySiretCode ?? quote.companySociety?.siretCode ?? ''],
      companyVatNumber: [quote.companyVatNumber ?? quote.companySociety?.vatNumber ?? ''],
      companyLegalInformation: [quote.companyLegalInformation ?? quote.companySociety?.legalInformation ?? ''],
      services:       servicesArray,
      vatRate:        [quote.quoteLines?.[0]?.vatRate ?? 20, Validators.required],
      validityPeriod: [quote.validityDate ? quote.validityDate.split('T')[0] : '', Validators.required],
      executionDate:  [quote.executionDate ? quote.executionDate.split('T')[0] : ''],
      status:         [status]
    });
  }

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) { this.router.navigate(['/fronthome/devis']); return; }

    this.currentUser = this.authenticateService.getCurrentUser() ?? undefined;

    forkJoin({
      quoteResp: this.quoteBll.getQuote$(id),
      users:     this.userBll.getUsers$(undefined, undefined, undefined, undefined, undefined, undefined, EnumRole.User)
    }).subscribe({
      next: ({ quoteResp, users }) => {
        const quote = quoteResp.quotes?.find(q => q.identifier === id) ?? quoteResp.quotes?.[0];
        if (!quote) { this.router.navigate(['/fronthome/devis']); return; }

        this.quote = quote;
        this.clients = users.users ?? [];
        this.quoteForm = this.buildForm(quote);
        this.selectedClientIdentifier = quote.clientUserIdentifier ?? null;

        const societyId = this.currentUser?.societyIdentifier ?? quote.companySocietyIdentifier;
        if (societyId) {
          this.societyBll.getSociety$(societyId).subscribe({
            next: (res: SocietyResponse) => {
              if (!res.society) return;
              this.currentSociety = res.society;

              const s = res.society;
              const addr = s.address;
              const addressParts = [addr?.number, addr?.addressLine, addr?.zipCode, addr?.city, addr?.country?.name]
                .filter(Boolean)
                .join(', ');

              this.quoteForm?.patchValue({
                companyName: this.quoteForm?.get('companyName')?.value || s.name,
                companyAddress: this.quoteForm?.get('companyAddress')?.value || addressParts,
                companyLogoPath: this.quoteForm?.get('companyLogoPath')?.value || s.logoPath || '',
                companyEmail: this.quoteForm?.get('companyEmail')?.value || s.email || '',
                companyPhone: this.quoteForm?.get('companyPhone')?.value || s.phone || '',
                companySiretCode: this.quoteForm?.get('companySiretCode')?.value || s.siretCode || '',
                companyVatNumber: this.quoteForm?.get('companyVatNumber')?.value || s.vatNumber || '',
                companyLegalInformation: this.quoteForm?.get('companyLegalInformation')?.value || s.legalInformation || ''
              });
            },
            error: (err: unknown) => console.error('Erreur chargement société', err)
          });
        }
      },
      error: (err: unknown) => {
        console.error('Erreur initialisation édition devis', err);
        this.router.navigate(['/fronthome/devis']);
      }
    });
  }

  onClientSelected(clientIdentifier: number): void {
    if (!this.quoteForm) {
      return;
    }

    this.selectedClientIdentifier = clientIdentifier;

    const user = this.clients.find(c => c.identifier === clientIdentifier);
    const clientName = user ? `${user.lastName} ${user.firstName}`.trim() : '';

    if (user?.address) {
      const a = user.address;
      const addressParts = [a.number, a.addressLine, a.zipCode, a.city, a.country?.name]
        .filter(Boolean)
        .join(', ');

      this.quoteForm.patchValue({
        clientIdentifier,
        clientName,
        clientAddress: addressParts
      });
      return;
    }

    this.quoteForm.patchValue({
      clientIdentifier,
      clientName,
      clientAddress: ''
    });
  }
}
