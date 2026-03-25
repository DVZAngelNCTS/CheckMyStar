import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { TableComponent } from '../../Components/Table/Table.component';
import { TableColumn } from '../../Components/Table/Models/TableColumn.model';
import { InvoiceModel } from '../../../20_Models/BackOffice/Invoice.model';
import { InvoiceBllService } from '../../../60_Bll/BackOffice/Invoice-bll.service';
import { FrontInvoiceFilterComponent } from './Filter/Invoice-filter.component';
import { InvoiceFilter } from '../../../30_Filters/BackOffice/Invoice.filter';
import { PopupComponent } from '../../Components/Popup/Popup.component';
import { ToastService } from '../../../90_Services/Toast/Toast.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-front-invoices-page',
  standalone: true,
  imports: [
    CommonModule,
    TranslationModule,
    TableComponent,
    FormsModule,
    ReactiveFormsModule,
    FrontInvoiceFilterComponent,
    PopupComponent
  ],
  templateUrl: './Invoices-page.component.html'
})
export class FrontInvoicesPageComponent implements OnInit {

  loading = false;
  loadingSearch = false;
  loadingReset = false;
  invoices: InvoiceModel[] = [];
  private allInvoices: InvoiceModel[] = [];

  popupVisible = false;
  popupMode: 'detail' | 'update' | 'delete' | 'enabled' | null = null;
  popupTitle = '';
  popupError: string | null = null;
  selectedInvoice: InvoiceModel | null = null;
  invoiceForm: FormGroup;

  columns = [
    {
      icon: 'bi bi-receipt',
      field: 'invoiceNumber',
      header: 'FrontInvoiceListSection.InvoiceNumber',
      sortable: true,
      filterable: true,
      width: '14%',
      pipe: (_, row) => this.getDisplayInvoiceNumber(row)
    },
    {
      icon: 'bi bi-calendar-event',
      field: 'invoiceDate',
      header: 'FrontInvoiceListSection.InvoiceDate',
      sortable: true,
      filterable: false,
      width: '12%',
      pipe: (_, row) => this.toFrDate(row.invoiceDate)
    },
    {
      icon: 'bi bi-calendar-check',
      field: 'dueDate',
      header: 'FrontInvoiceListSection.DueDate',
      sortable: true,
      filterable: false,
      width: '12%',
      pipe: (_, row) => this.toFrDate(row.dueDate)
    },
    {
      icon: 'bi bi-currency-euro',
      field: 'totalAmountTTC',
      header: 'FrontInvoiceListSection.TotalAmountTTC',
      sortable: true,
      filterable: false,
      width: '14%',
      pipe: (_, row) => row.totalAmountTTC != null ? `${row.totalAmountTTC.toFixed(2)} €` : ''
    },
    {
      icon: 'bi bi-wallet2',
      field: 'paymentStatusIdentifier',
      header: 'FrontInvoiceListSection.PaymentStatus',
      sortable: true,
      filterable: false,
      width: '12%',
      pipe: (_, row) => this.getPaymentStatusLabel(row.paymentStatusIdentifier)
    },
    {
      icon: 'bi bi-toggle2-on',
      field: 'isActive',
      header: 'FrontInvoiceListSection.Active',
      sortable: true,
      filterable: false,
      width: '8%'
    }
  ] as TableColumn<InvoiceModel>[];

  constructor(
    private invoiceBll: InvoiceBllService,
    private toast: ToastService,
    private translate: TranslateService,
    private fb: FormBuilder
  ) {
    this.invoiceForm = this.fb.group({
      invoiceNumber: ['', [Validators.required]],
      invoiceDate: [this.todayIsoDate(), [Validators.required]],
      dueDate: [this.todayIsoDate(), [Validators.required]],
      totalAmountHT: [0, [Validators.required, Validators.min(0)]],
      totalVATAmount: [0, [Validators.required, Validators.min(0)]],
      totalAmountTTC: [0, [Validators.required, Validators.min(0)]],
      paymentStatusIdentifier: [1, [Validators.required]],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices(filter?: InvoiceFilter): void {
    this.invoiceBll.getInvoicesByInspector$(filter?.invoiceNumber, filter?.paymentStatusIdentifier ?? undefined, filter?.isActive ?? undefined).subscribe({
      next: response => {
        this.allInvoices = response.invoices ?? [];
        this.invoices = this.applyClientFilters(this.allInvoices, filter);
      },
      error: err => console.error(err)
    });
  }

  onFilter(filter: InvoiceFilter): void {
    if (filter.reset) {
      this.loadingReset = true;
    } else {
      this.loadingSearch = true;
    }

    const filterToApply: InvoiceFilter | undefined = filter.reset ? undefined : filter;

    this.invoiceBll.getInvoicesByInspector$(filterToApply?.invoiceNumber, filterToApply?.paymentStatusIdentifier ?? undefined, filterToApply?.isActive ?? undefined).subscribe({
      next: response => {
        if (filter.reset) this.loadingReset = false;
        else this.loadingSearch = false;
        this.allInvoices = response.invoices ?? [];
        this.invoices = this.applyClientFilters(this.allInvoices, filterToApply);
      },
      error: err => {
        if (filter.reset) this.loadingReset = false;
        else this.loadingSearch = false;
        console.error(err);
      }
    });
  }

  onDetail(invoice: InvoiceModel): void {
    this.loading = false;
    this.popupError = null;
    this.selectedInvoice = invoice;
    this.popupMode = 'detail';
    this.popupTitle = this.translate.instant('FrontInvoiceListSection.Preview');
    this.popupVisible = true;
  }

  onUpdate(invoice: InvoiceModel): void {
    this.loading = false;
    this.popupError = null;
    this.selectedInvoice = invoice;
    this.patchForm(invoice);
    this.popupMode = 'update';
    this.popupTitle = this.translate.instant('FrontInvoiceListSection.Update');
    this.popupVisible = true;
  }

  onDelete(invoice: InvoiceModel): void {
    this.loading = false;
    this.popupError = null;
    this.selectedInvoice = invoice;
    this.popupMode = 'delete';
    this.popupTitle = this.translate.instant('FrontInvoiceListSection.Delete');
    this.popupVisible = true;
  }

  toggleEnabled(invoice: InvoiceModel): void {
    this.loading = false;
    this.popupError = null;
    this.selectedInvoice = invoice;
    this.popupMode = 'enabled';
    this.popupTitle = this.translate.instant(invoice.isActive ? 'FrontInvoiceListSection.Disable' : 'FrontInvoiceListSection.Enable');
    this.popupVisible = true;
  }

  onCreate(): void {
    // La création ouvrira une sous-page dédiée ultérieurement.
  }

  onPopupConfirm(): void {
    if (this.popupMode === 'detail') {
      this.onPopupCancel();
      return;
    }
    if (this.popupMode === 'delete') {
      this.onDeleteConfirmed();
      return;
    }
    if (this.popupMode === 'enabled') {
      this.onEnabledConfirmed();
      return;
    }
    if (this.popupMode === 'update') {
      this.onUpdateConfirmed();
    }
  }

  onPopupCancel(): void {
    this.popupVisible = false;
    this.popupError = null;
    this.selectedInvoice = null;
    this.popupMode = null;
    this.loading = false;
  }

  getPopupConfirmLabel(): string {
    if (this.popupMode === 'detail') {
      return this.translate.instant('PopupSection.Close');
    }
    if (this.popupMode === 'update') {
      return this.translate.instant('FrontInvoiceListSection.UpdateAction');
    }
    return this.translate.instant('PopupSection.Validate');
  }

  private onDeleteConfirmed(): void {
    if (!this.selectedInvoice) return;
    this.loading = true;

    this.invoiceBll.deleteInvoice$(this.selectedInvoice.identifier).subscribe({
      next: response => {
        this.loading = false;
        if (!response.isSuccess) {
          this.popupError = response.message;
          return;
        }
        this.popupVisible = false;
        this.toast.show(response.message, 'success', 5000);
        this.loadInvoices();
      },
      error: err => {
        this.loading = false;
        this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  private onEnabledConfirmed(): void {
    if (!this.selectedInvoice) return;
    this.loading = true;

    const updated: InvoiceModel = {
      ...this.selectedInvoice,
      isActive: !this.selectedInvoice.isActive
    };

    this.invoiceBll.updateInvoice$(updated).subscribe({
      next: response => {
        this.loading = false;
        if (!response.isSuccess) {
          this.popupError = response.message;
          return;
        }
        this.popupVisible = false;
        this.toast.show(response.message, 'success', 5000);
        this.loadInvoices();
      },
      error: err => {
        this.loading = false;
        this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  private onUpdateConfirmed(): void {
    if (!this.selectedInvoice) return;
    if (this.invoiceForm.invalid) {
      this.invoiceForm.markAllAsTouched();
      this.popupError = this.translate.instant('FrontInvoiceListSection.InvalidForm');
      return;
    }

    this.loading = true;
    this.popupError = null;

    const invoiceToUpdate = this.buildInvoiceFromForm(this.selectedInvoice);

    this.invoiceBll.updateInvoice$(invoiceToUpdate).subscribe({
      next: response => {
        this.loading = false;
        if (!response.isSuccess) {
          this.popupError = response.message;
          return;
        }
        this.popupVisible = false;
        this.toast.show(response.message, 'success', 5000);
        this.loadInvoices();
      },
      error: err => {
        this.loading = false;
        this.popupError = err.error?.message || this.translate.instant('CommonSection.UnknownError');
      }
    });
  }

  private buildInvoiceFromForm(base?: InvoiceModel): InvoiceModel {
    const form = this.invoiceForm.value;

    return {
      identifier: base?.identifier ?? 0,
      invoiceNumber: form.invoiceNumber,
      quoteIdentifier: base?.quoteIdentifier ?? 0,
      clientUserIdentifier: base?.clientUserIdentifier ?? 0,
      clientAddressIdentifier: base?.clientAddressIdentifier ?? 0,
      companySocietyIdentifier: base?.companySocietyIdentifier ?? 0,
      companyAddressIdentifier: base?.companyAddressIdentifier ?? 0,
      invoiceDate: form.invoiceDate,
      totalAmountHT: Number(form.totalAmountHT ?? 0),
      totalVATAmount: Number(form.totalVATAmount ?? 0),
      totalAmountTTC: Number(form.totalAmountTTC ?? 0),
      paymentStatusIdentifier: Number(form.paymentStatusIdentifier ?? 1),
      dueDate: form.dueDate,
      createdDate: base?.createdDate,
      updatedDate: base?.updatedDate,
      isActive: !!form.isActive,
      invoiceLines: base?.invoiceLines ?? []
    };
  }

  private patchForm(invoice: InvoiceModel): void {
    this.invoiceForm.patchValue({
      invoiceNumber: invoice.invoiceNumber ?? '',
      invoiceDate: this.toIsoDate(invoice.invoiceDate),
      dueDate: this.toIsoDate(invoice.dueDate),
      totalAmountHT: invoice.totalAmountHT ?? 0,
      totalVATAmount: invoice.totalVATAmount ?? 0,
      totalAmountTTC: invoice.totalAmountTTC ?? 0,
      paymentStatusIdentifier: invoice.paymentStatusIdentifier ?? 1,
      isActive: invoice.isActive ?? true
    });
  }

  private applyClientFilters(invoices: InvoiceModel[], filter?: InvoiceFilter): InvoiceModel[] {
    if (!filter) return invoices;

    let result = invoices;

    if (filter.invoiceDateFrom) {
      const from = new Date(filter.invoiceDateFrom);
      result = result.filter(i => i.invoiceDate ? new Date(i.invoiceDate) >= from : true);
    }
    if (filter.invoiceDateTo) {
      const to = new Date(filter.invoiceDateTo);
      result = result.filter(i => i.invoiceDate ? new Date(i.invoiceDate) <= to : true);
    }
    if (filter.dueDateFrom) {
      const from = new Date(filter.dueDateFrom);
      result = result.filter(i => i.dueDate ? new Date(i.dueDate) >= from : true);
    }
    if (filter.dueDateTo) {
      const to = new Date(filter.dueDateTo);
      result = result.filter(i => i.dueDate ? new Date(i.dueDate) <= to : true);
    }
    if (filter.totalAmountTTCMin != null) {
      result = result.filter(i => (i.totalAmountTTC ?? 0) >= filter.totalAmountTTCMin!);
    }
    if (filter.totalAmountTTCMax != null) {
      result = result.filter(i => (i.totalAmountTTC ?? 0) <= filter.totalAmountTTCMax!);
    }

    return result;
  }

  private getDisplayInvoiceNumber(invoice: InvoiceModel): string {
    if (invoice.invoiceNumber) return invoice.invoiceNumber;
    const year = invoice.createdDate ? new Date(invoice.createdDate).getFullYear() : new Date().getFullYear();
    const counter = String(invoice.identifier).padStart(4, '0');
    return `FAC-${year}-${counter}`;
  }

  private getPaymentStatusLabel(status?: number): string {
    const map: Record<number, string> = {
      1: this.translate.instant('FrontInvoiceListSection.PaymentPending'),
      2: this.translate.instant('FrontInvoiceListSection.PaymentPaid')
    };
    return map[status ?? 0] ?? this.translate.instant('FrontInvoiceListSection.PaymentUnknown');
  }

  private todayIsoDate(): string {
    return new Date().toISOString().slice(0, 10);
  }

  private toIsoDate(value?: string): string {
    if (!value) return this.todayIsoDate();
    return new Date(value).toISOString().slice(0, 10);
  }

  private toFrDate(value?: string): string {
    if (!value) return '';
    return new Date(value).toLocaleDateString('fr-FR');
  }
}
