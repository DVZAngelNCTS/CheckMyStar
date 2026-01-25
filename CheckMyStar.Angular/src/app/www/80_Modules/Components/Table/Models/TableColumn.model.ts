export interface TableColumn<T> {
  icon: string;
  field: keyof T;
  header: string;
  width?: string;
  translate?: boolean;
  sortable?: boolean;
  defaultSort?: boolean;
  sortDirection?: 'asc' | 'desc';
  filterable?: boolean;
}
