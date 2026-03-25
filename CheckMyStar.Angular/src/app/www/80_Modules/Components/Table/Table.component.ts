import { Component, Input, input, output, signal, OnChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { TableColumn } from './Models/TableColumn.model';
import { CsvExportService } from '../../../90_Services/Export/Csv-export.service';
import { XlsxExportService } from '../../../90_Services/Export/Xlsx-export.service';
import { DeviceService } from '../../../90_Services/Device/Device.service';
import { TooltipDirective } from '../Tooltip/Tooltip.directive';
import { CriterionCategory } from '../../../10_Common/Utils/Criterion-category.util';

@Component({ 
  selector: 'app-table', 
  standalone: true, 
  imports: [CommonModule, RouterModule, TranslationModule, TooltipDirective], 
  templateUrl: './Table.component.html',
  styleUrls: ['./Table.component.css']
})
export class TableComponent<T> implements OnChanges {

  detail = output<T>();
  update = output<T>();
  delete = output<T>();
  enabled = output<T>();
  rowClick = output<T>();
  openCreate = output<void>();
  showExplanation = output<T>();
  customAction = output<T>();

  @Input() showDetail = true;
  @Input() showUpdate = true;
  @Input() showEnabled = true;
  @Input() showDelete = true;
  @Input() showCsvExport = true;
  @Input() showXlsxExport = true;
  @Input() showAdd = true;
  @Input() showExplanationBtn = false;
  @Input() showCustomAction = false;
  @Input() customActionIcon = 'bi bi-star';
  @Input() customActionTooltip = '';
  
  columns = input<TableColumn<T>[]>([]);
  data = input<T[]>([]);
  rowLink = input<((row: T) => any[]) | null>(null);
  categoryFn = input<((row: T) => CriterionCategory | null) | null>(null);
  /** When set, category headers are only shown while sorting by this field ascending. */
  categoryField = input<string | null>(null);

  displayData = signal<T[]>([]);

  sortField: keyof T | string | null = null;
  sortDirection: 'asc' | 'desc' = 'asc';

  filters: Record<string, string> = {};

  constructor(
    private csv: CsvExportService,
    private xlsx: XlsxExportService,
    public device: DeviceService
  ) {}

  ngAfterViewInit() {
    this.initTooltips();
  }

  ngOnChanges() {
    this.initTooltips();
    this.displayData.set(this.data());

    const defaultCol = this.columns().find(c => c.defaultSort);

    if (defaultCol) {
      this.sortField = defaultCol.field;
      this.sortDirection = defaultCol.sortDirection ?? 'asc';
      this.applySort(defaultCol);
      return;
    }

    const firstSortable = this.columns().find(c => c.sortable);
    if (firstSortable && !this.sortField) {
      this.sortField = firstSortable.field;
      this.sortDirection = 'asc';
      this.applySort(firstSortable);
    }
  }

  /** Permet d'accéder à row[col.field] même si field est virtuel */
  getCellValue(row: T, col: TableColumn<T>) {
    return (row as any)[col.field];
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
      const v1 = this.getCellValue(a, col);
      const v2 = this.getCellValue(b, col);

      return this.sortDirection === 'asc'
        ? (v1 > v2 ? 1 : -1)
        : (v1 < v2 ? 1 : -1);
    });

    this.displayData.set(sorted);
  }

  onFilterChange(col: TableColumn<T>, value: string) {
    this.filters[String(col.field)] = value.toLowerCase();

    const filtered = this.data().filter(row =>
      Object.entries(this.filters).every(([field, filterValue]) =>
        !filterValue || String((row as any)[field] ?? '').toLowerCase().includes(filterValue)
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

  initTooltips() {
    setTimeout(() => {
      document.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(el => {
        bootstrap.Tooltip.getInstance(el)?.dispose();
        new bootstrap.Tooltip(el);
      });
    }, 0);
  }

  getRowLink(row: T): any[] | null {
    const fn = this.rowLink();
    return fn ? fn(row) : null;
  }

  /**
   * Returns the category header for the row at given index if it differs from the previous row's category.
   * Returns null if categoryFn is not set or if the category hasn't changed.
   * When categoryField is set, categories are only shown while sorting by that field ascending.
   */
  getCategoryHeaderForRow(index: number): CriterionCategory | null {
    const fn = this.categoryFn();
    if (!fn) return null;
    const field = this.categoryField();
    if (field && (this.sortField !== field || this.sortDirection !== 'asc')) return null;
    const items = this.displayData();
    const current = fn(items[index]);
    if (!current) return null;
    if (index === 0) return current;
    const previous = fn(items[index - 1]);
    if (!previous || previous.category !== current.category) return current;
    return null;
  }

  get totalColumns(): number {
    let count = this.columns().length;
    if (this.showCustomAction) count++;
    if (this.showExplanationBtn) count++;
    if (this.showDetail) count++;
    if (this.showUpdate) count++;
    if (this.showEnabled || this.showCsvExport) count++;
    if (this.showDelete || this.showXlsxExport) count++;
    return count;
  }
}
