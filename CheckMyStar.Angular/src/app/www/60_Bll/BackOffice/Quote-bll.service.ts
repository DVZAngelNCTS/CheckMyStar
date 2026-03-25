import { Injectable } from '@angular/core';
import { QuoteDalService } from '../../70_Dal/BackOffice/Quote-dal.service';
import { QuoteGetRequest } from '../../40_Requests/BackOffice/Quote-get.request';
import { QuoteSaveRequest } from '../../40_Requests/BackOffice/Quote-save.request';
import { QuoteDeleteRequest } from '../../40_Requests/BackOffice/Quote-delete.request';
import { QuoteModel } from '../../20_Models/BackOffice/Quote.model';

@Injectable({
  providedIn: 'root'
})
export class QuoteBllService {
  constructor(private quoteDal: QuoteDalService) {}

  getQuotes$(reference?: string, isAccepted?: boolean) {
    const request: QuoteGetRequest = {
      reference: reference,
      isAccepted: isAccepted
    };

    return this.quoteDal.getQuotes$(request);
  }

  getQuote$(quoteIdentifier: number) {
    const request: QuoteGetRequest = {
      identifier: quoteIdentifier,
      quoteIdentifier
    };
    return this.quoteDal.getQuotes$(request);
  }

  getNextIdentifier$() {
    return this.quoteDal.getNextIdentifier$();
  }

  addQuote$(quote: QuoteModel) {
    const request = { quote: quote } as QuoteSaveRequest;
    return this.quoteDal.addQuote$(request);
  }

  updateQuote$(quote: QuoteModel) {
    const request = { quote: quote } as QuoteSaveRequest;
    return this.quoteDal.updateQuote$(request);
  }

  deleteQuote$(quoteIdentifier: number) {
    const request: QuoteDeleteRequest = { identifier: quoteIdentifier };
    return this.quoteDal.deleteQuote$(request);
  }

  enabledQuote$(quote: QuoteModel) {
    const request = { quote: quote } as QuoteSaveRequest;
    return this.quoteDal.enabledQuote$(request);
  }

  generatePdf$(identifier: number) {
    return this.quoteDal.generatePdf$(identifier);
  }
}
