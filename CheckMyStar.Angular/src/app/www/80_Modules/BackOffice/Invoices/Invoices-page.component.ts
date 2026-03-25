import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { InvoiceFilterComponent } from './Filter/Invoice-filter.component';
import { TableComponent } from '../../Components/Table/Table.component';
import { TableColumn } from '../../Components/Table/Models/TableColumn.model';
import { InvoiceModel } from '../../../20_Models/BackOffice/Invoice.model';
import { InvoiceFilter } from '../../../30_Filters/BackOffice/Invoice.filter';
import { InvoiceBllService } from '../../../60_Bll/BackOffice/Invoice-bll.service';

@Component({
  selector: 'app-invoices-page',
  standalone: true,
  imports: [CommonModule, TranslationModule, InvoiceFilterComponent, TableComponent],
  templateUrl: './Invoices-page.component.html'
})
export class InvoicesPageComponent implements OnInit {
  loadingSearch = false;
  loadingReset = false;
  invoices: InvoiceModel[] = [];

  columns = [
    {
      icon: 'bi bi-receipt',
      field: 'invoiceNumber',
      header: 'FrontInvoiceListSection.InvoiceNumber',
      sortable: true,
      filterable: true,
      width: '16%',
      pipe: (_: unknown, row: InvoiceModel) => row.invoiceNumber ?? this.getLegacyInvoiceNumber(row)
    },
    {
      icon: 'bi bi-calendar-event',
      field: 'invoiceDate',
      header: 'FrontInvoiceListSection.InvoiceDate',
      sortable: true,
      filterable: true,
      width: '14%',
      pipe: (_: unknown, row: InvoiceModel) => this.toFrDate(row.invoiceDate)
    },
    {
      icon: 'bi bi-calendar-check',
      field: 'dueDate',
      header: 'FrontInvoiceListSection.DueDate',
      sortable: true,
      filterable: true,
      width: '14%',
      pipe: (_: unknown, row: InvoiceModel) => this.toFrDate(row.dueDate)
    },
    {
      icon: 'bi bi-currency-euro',
      field: 'totalAmountTTC',
      header: 'FrontInvoiceListSection.TotalAmountTTC',
      sortable: true,
      filterable: true,
      width: '16%',
      pipe: (_: unknown, row: InvoiceModel) => row.totalAmountTTC != null ? `${row.totalAmountTTC.toFixed(2)} €` : ''
    },
    {
      icon: 'bi bi-wallet2',
      field: 'paymentStatusIdentifier',
      header: 'FrontInvoiceListSection.PaymentStatus',
      sortable: true,
      filterable: false,
      width: '14%',
      pipe: (_: unknown, row: InvoiceModel) => this.getPaymentStatusLabel(row.paymentStatusIdentifier)
    },
    {
      icon: 'bi bi-toggle2-on',
      field: 'isActive',
      header: 'FrontInvoiceListSection.Active',
      sortable: true,
      filterable: false,
      width: '10%'
    }
  ] as TableColumn<InvoiceModel>[];

  constructor(private invoiceBll: InvoiceBllService) {}

  ngOnInit(): void {
    this.loadInvoices();
  }

  loadInvoices(filter?: InvoiceFilter): void {
    const appliedFilter = filter?.reset ? undefined : filter;
    const paymentStatusIdentifier = appliedFilter?.paymentStatusIdentifier == null ? undefined : Number(appliedFilter.paymentStatusIdentifier);
    const isActive = appliedFilter?.isActive == null ? undefined : appliedFilter.isActive;

    this.invoiceBll.getInvoices$(appliedFilter?.invoiceNumber, paymentStatusIdentifier, isActive).subscribe({
      next: response => {
        this.loadingSearch = false;
        this.loadingReset = false;
        this.invoices = response.invoices ?? [];
      },
      error: err => {
        this.loadingSearch = false;
        this.loadingReset = false;
        console.error(err);
      }
    });
  }

  onFilter(filter: InvoiceFilter): void {
    if (filter.reset) {
      this.loadingReset = true;
    } else {
      this.loadingSearch = true;
    }

    this.loadInvoices(filter);
  }

  private toFrDate(value?: string): string {
    if (!value) {
      return '';
    }

    const date = new Date(value);
    return Number.isNaN(date.getTime()) ? '' : date.toLocaleDateString('fr-FR');
  }

  private getPaymentStatusLabel(status?: number): string {
    if (status === 2) {
      return 'Payée';
    }
    if (status === 1) {
      return 'En attente';
    }
    return '';
  }

  private getLegacyInvoiceNumber(invoice: InvoiceModel): string {
    if (invoice.number) {
      return invoice.number;
    }

    return String(invoice.identifier).padStart(6, '0');
  }
}
