export interface InvoiceFilter {
  invoiceNumber?: string;
  paymentStatusIdentifier?: number | null;
  invoiceDateFrom?: string;
  invoiceDateTo?: string;
  dueDateFrom?: string;
  dueDateTo?: string;
  totalAmountTTCMin?: number | null;
  totalAmountTTCMax?: number | null;
  isActive?: boolean | null;
  reset?: boolean;
}
