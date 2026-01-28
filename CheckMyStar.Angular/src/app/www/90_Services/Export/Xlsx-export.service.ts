import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx-js-style';

@Injectable({
  providedIn: 'root'
})
export class XlsxExportService {

  exportToExcel(filename: string, rows: any[]): void {
    if (!rows || rows.length === 0) {
      console.warn('Aucune donnée à exporter');
      return;
    }

    const worksheet = XLSX.utils.json_to_sheet(rows);

    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'data');

    // 👉 Méthode moderne, compatible Chrome
    XLSX.writeFileXLSX(workbook, `${filename}.xlsx`, {
      compression: true,
      bookType: 'xlsx'
    });
  }
}
