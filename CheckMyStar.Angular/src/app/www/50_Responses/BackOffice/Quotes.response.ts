import { QuoteModel } from '../../20_Models/BackOffice/Quote.model';
import { BaseResponse } from '../BaseResponse';

export interface QuotesResponse extends BaseResponse {
  quotes?: QuoteModel[];
}
