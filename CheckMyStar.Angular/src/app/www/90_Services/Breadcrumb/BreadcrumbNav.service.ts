import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BreadcrumbNavService {
  customBackAction: (() => void) | null = null;
}
