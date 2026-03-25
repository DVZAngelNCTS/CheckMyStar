import { QuoteModel } from '../../20_Models/BackOffice/Quote.model';
import { BaseResponse } from '../BaseResponse';

export interface QuoteResponse extends BaseResponse {
  nextIdentifier?: string;
  quote?: QuoteModel;
}
