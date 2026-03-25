import { InvoiceModel } from '../../20_Models/BackOffice/Invoice.model';
import { BaseResponse } from '../BaseResponse';

export interface InvoicesResponse extends BaseResponse {
  invoices?: InvoiceModel[];
}
