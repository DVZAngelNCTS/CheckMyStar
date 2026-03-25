import { Injectable } from '@angular/core';
import { InvoiceDalService } from '../../70_Dal/BackOffice/Invoice-dal.service';
import { InvoiceGetRequest } from '../../40_Requests/BackOffice/Invoice-get.request';
import { InvoiceSaveRequest } from '../../40_Requests/BackOffice/Invoice-save.request';
import { InvoiceDeleteRequest } from '../../40_Requests/BackOffice/Invoice-delete.request';
import { InvoiceModel } from '../../20_Models/BackOffice/Invoice.model';

@Injectable({
  providedIn: 'root'
})
export class InvoiceBllService {
  constructor(private invoiceDal: InvoiceDalService) {}

  getInvoices$(invoiceNumber?: string, paymentStatusIdentifier?: number, isActive?: boolean) {
    const request: InvoiceGetRequest = {
      invoiceNumber,
      paymentStatusIdentifier,
      isActive
    };

    return this.invoiceDal.getInvoices$(request);
  }

  getInvoicesByInspector$(invoiceNumber?: string, paymentStatusIdentifier?: number, isActive?: boolean) {
    const request: InvoiceGetRequest = {
      invoiceNumber,
      paymentStatusIdentifier,
      isActive
    };

    return this.invoiceDal.getInvoicesByInspector$(request);
  }

  getInvoice$(invoiceIdentifier: number) {
    const request: InvoiceGetRequest = {
      identifier: invoiceIdentifier,
      invoiceIdentifier
    };
    return this.invoiceDal.getInvoices$(request);
  }

  getNextIdentifier$() {
    return this.invoiceDal.getNextIdentifier$();
  }

  addInvoice$(invoice: InvoiceModel) {
    const request = { invoice } as InvoiceSaveRequest;
    return this.invoiceDal.addInvoice$(request);
  }

  updateInvoice$(invoice: InvoiceModel) {
    const request = { invoice } as InvoiceSaveRequest;
    return this.invoiceDal.updateInvoice$(request);
  }

  deleteInvoice$(invoiceIdentifier: number) {
    const request: InvoiceDeleteRequest = { identifier: invoiceIdentifier };
    return this.invoiceDal.deleteInvoice$(request);
  }
}
