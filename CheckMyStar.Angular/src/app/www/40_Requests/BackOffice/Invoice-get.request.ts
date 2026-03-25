export interface InvoiceGetRequest {
  identifier?: number;
  invoiceIdentifier?: number;
  invoiceNumber?: string;
  quoteIdentifier?: number;
  clientUserIdentifier?: number;
  paymentStatusIdentifier?: number;
  isActive?: boolean;
}
