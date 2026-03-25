import { InvoiceModel } from '../../20_Models/BackOffice/Invoice.model';
import { BaseResponse } from '../BaseResponse';

export interface InvoiceResponse extends BaseResponse {
  nextIdentifier?: string;
  invoice?: InvoiceModel;
}
