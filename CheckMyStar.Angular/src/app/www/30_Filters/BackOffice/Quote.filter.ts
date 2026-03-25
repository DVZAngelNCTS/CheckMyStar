export interface QuoteFilter {
  reference?: string;
  isAccepted?: boolean | null;
  createdDateFrom?: string;
  createdDateTo?: string;
  totalAmountTTCMin?: number | null;
  totalAmountTTCMax?: number | null;
  validityDateFrom?: string;
  validityDateTo?: string;
  reset?: boolean;
}
