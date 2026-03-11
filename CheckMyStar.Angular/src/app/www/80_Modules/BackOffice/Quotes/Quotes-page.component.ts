import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
  selector: 'app-quotes-page',
  standalone: true,
  imports: [CommonModule, TranslationModule],
  templateUrl: './Quotes-page.component.html'
})
export class QuotesPageComponent {}
