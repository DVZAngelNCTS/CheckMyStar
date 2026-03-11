import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
  selector: 'app-invoices-page',
  standalone: true,
  imports: [CommonModule, TranslationModule],
  templateUrl: './Invoices-page.component.html'
})
export class InvoicesPageComponent {}
