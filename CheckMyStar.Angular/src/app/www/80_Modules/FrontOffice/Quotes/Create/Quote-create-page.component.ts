import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormArray, Validators } from '@angular/forms';
import { QuoteBllService } from '../../../../60_Bll/BackOffice/Quote-bll.service';
import { UserBllService } from '../../../../60_Bll/BackOffice/User-bll.service';
import { SocietyBllService } from '../../../../60_Bll/BackOffice/Society-bll.service';
import { SocietyResponse } from '../../../../50_Responses/BackOffice/Society.response';
import { SocietyModel } from '../../../../20_Models/BackOffice/Society.model';
import { UserModel } from '../../../../20_Models/Common/User.model';
import { EnumRole } from '../../../../10_Common/Enumerations/EnumRole';
import { AuthenticateService } from '../../../../90_Services/Authenticate/Authenticate.service';
import { forkJoin } from 'rxjs';
import { QuoteCreateFormComponent } from './Quote-create-form.component';
import { QuoteCreatePreviewComponent } from './Quote-create-preview.component';

@Component({
  selector: 'app-front-quote-create-page',
  standalone: true,
  imports: [CommonModule, QuoteCreateFormComponent, QuoteCreatePreviewComponent],
  template: `
    <div class="quotes-layout">
      <app-quote-create-form
        [quoteForm]="quoteForm"
        [clients]="clients"
        [selectedClientIdentifier]="selectedClientIdentifier"
        (clientSelected)="onClientSelected($event)">
      </app-quote-create-form>
      <app-quote-create-preview [quoteForm]="quoteForm" [currentUser]="currentUser" [clients]="clients" [currentSociety]="currentSociety"></app-quote-create-preview>
    </div>
  `,
  styleUrls: ['./Quote-create-page.component.css']
})
export class FrontQuoteCreatePageComponent implements OnInit {

  quoteForm: FormGroup;
  clients: UserModel[] = [];
  selectedClientIdentifier: number | null = null;
  currentUser: UserModel | undefined;
  currentSociety: SocietyModel | undefined;

  constructor(
    private fb: FormBuilder,
    private quoteBll: QuoteBllService,
    private userBll: UserBllService,
    private societyBll: SocietyBllService,
    private authenticateService: AuthenticateService
  ) {
    const today = new Date().toISOString().split('T')[0];
    this.quoteForm = this.fb.group({
      quoteNumber:    ['', Validators.required],
      quoteDate:      [today, Validators.required],
      clientIdentifier: [null, Validators.required],
      clientName:     ['', Validators.required],
      clientAddress:  [''],
      companyName:    ['', Validators.required],
      companyAddress: [''],
      companyLogoPath: [''],
      companyEmail: [''],
      companyPhone: [''],
      companySiretCode: [''],
      companyVatNumber: [''],
      companyLegalInformation: [''],
      services:       this.fb.array([this.createService()]),
      vatRate:        [20, Validators.required],
      validityPeriod: ['', Validators.required],
      executionDate:  [''],
      status:         ['draft']
    });
  }

  private createService(): FormGroup {
    return this.fb.group({
      description: ['', Validators.required],
      quantity:    [1, [Validators.required, Validators.min(0)]],
      unit:        [''],
      unitPrice:   [0, [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit(): void {
    this.currentUser = this.authenticateService.getCurrentUser() ?? undefined;

    forkJoin({
      nextId:  this.quoteBll.getNextIdentifier$(),
      users:   this.userBll.getUsers$(undefined, undefined, undefined, undefined, undefined, undefined, EnumRole.User)
    }).subscribe({
      next: ({ nextId, users }) => {
        const id = nextId.quote?.identifier;
        if (id != null) {
          const year = new Date().getFullYear();
          const padded = String(id).padStart(4, '0');
          this.quoteForm.patchValue({ quoteNumber: `DEV-${year}-${padded}` });
        }

        this.clients = users.users ?? [];

        const societyId = this.currentUser?.societyIdentifier;
        if (societyId) {
          this.societyBll.getSociety$(societyId).subscribe({
            next: (res: SocietyResponse) => {
              if (!res.society) return;
              this.currentSociety = res.society;
              const s = res.society;
              const addr = s.address;
              const addressParts = [addr?.number, addr?.addressLine, addr?.zipCode, addr?.city, addr?.country?.name]
                .filter(Boolean).join(', ');
              this.quoteForm.patchValue({
                companyName: s.name,
                companyAddress: addressParts,
                companyLogoPath: s.logoPath ?? '',
                companyEmail: s.email ?? '',
                companyPhone: s.phone ?? '',
                companySiretCode: s.siretCode ?? '',
                companyVatNumber: s.vatNumber ?? '',
                companyLegalInformation: s.legalInformation ?? ''
              });
            },
            error: (err: unknown) => console.error('Erreur chargement société', err)
          });
        }
      },
      error: (err: unknown) => console.error('Erreur initialisation devis', err)
    });
  }

  onClientSelected(clientIdentifier: number): void {
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
