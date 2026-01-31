import { Component, input, output, signal, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { TableColumn } from './Models/TableColumn.model';
import { CsvExportService } from '../../../90_Services/Export/Csv-export.service';
import { XlsxExportService } from '../../../90_Services/Export/Xlsx-export.service';

@Component({ 
  selector: 'app-table', 
  standalone: true, 
  imports: [CommonModule, TranslationModule], 
  templateUrl: './Table.component.html',
  styleUrls: ['./Table.component.css']
})
export class TableComponent<T> implements OnChanges {
  update = output<T>();
  delete = output<T>();
  enabled = output<T>();

  columns = input<TableColumn<T>[]>([]);
  data = input<T[]>([]);

  displayData = signal<T[]>([]);

  sortField: keyof T | null = null;
  sortDirection: 'asc' | 'desc' = 'asc';

  filters: Record<string, string> = {};

  constructor(private csv: CsvExportService, private xlsx: XlsxExportService) {

  }

  ngOnChanges() {
    this.displayData.set(this.data());

    // Tri premium : appliquer un tri par défaut si défini
    const defaultCol = this.columns().find(c => c.defaultSort);

    if (defaultCol) {
      this.sortField = defaultCol.field;
      this.sortDirection = defaultCol.sortDirection ?? 'asc';
      this.applySort(defaultCol);
      return;
    }

    // Sinon : prendre la première colonne triable
    const firstSortable = this.columns().find(c => c.sortable);
    if (firstSortable && !this.sortField) {
      this.sortField = firstSortable.field;
      this.sortDirection = 'asc';
      this.applySort(firstSortable);
    }
  }

  onSort(col: TableColumn<T>) {
    if (!col.sortable) return;

    if (this.sortField === col.field) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortField = col.field;
      this.sortDirection = 'asc';
    }

    this.applySort(col);
  }

  private applySort(col: TableColumn<T>) {
    const sorted = [...this.displayData()].sort((a, b) => {
      const v1 = a[col.field];
      const v2 = b[col.field];

      return this.sortDirection === 'asc'
        ? (v1 > v2 ? 1 : -1)
        : (v1 < v2 ? 1 : -1);
    });

    this.displayData.set(sorted);
  }

  onFilterChange(col: TableColumn<T>, value: string) {
    this.filters[col.field as string] = value.toLowerCase();

    const filtered = this.data().filter(row =>
        Object.entries(this.filters).every(([field, filterValue]) =>
        !filterValue || String(row[field as keyof T]).toLowerCase().includes(filterValue)
        )
    );

    this.displayData.set(filtered);
    }

    exportCsv() { 
      this.csv.exportToCsv('export', this.displayData()); 
    } 
    
    exportXlsx() { 
      this.xlsx.exportToExcel('export', this.displayData()); 
    }
}
