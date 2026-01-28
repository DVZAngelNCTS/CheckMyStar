import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class CsvExportService {

  exportToCsv(filename: string, rows: any[]): void {
    if (!rows || rows.length === 0) {
      console.warn('Aucune donnée à exporter');
      return;
    }

    const separator = ';';
    const keys = Object.keys(rows[0]);

    const csvContent =
      keys.join(separator) +
      '\n' +
      rows
        .map(row =>
          keys
            .map(k => {
              let cell = row[k] === null || row[k] === undefined ? '' : row[k];
              cell = cell instanceof Date ? cell.toLocaleString() : cell.toString().replace(/"/g, '""');
              return `"${cell}"`;
            })
            .join(separator)
        )
        .join('\n');

    // 👉 BOM UTF‑8 pour Excel Windows
    const bom = '\uFEFF';

    const blob = new Blob([bom + csvContent], {
      type: 'text/csv;charset=utf-8;'
    });

    const link = document.createElement('a');
    link.href = URL.createObjectURL(blob);
    link.download = filename + '.csv';
    link.click();
  }
}
