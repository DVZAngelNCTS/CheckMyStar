import { Component, output, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder } from '@angular/forms';
import { TranslationModule } from '../../../../10_Common/Translation.module';
import { FolderFilter } from '../../../../30_Filters/BackOffice/Folder.filter';
import { FieldComponent } from '../../../Components/Field/Field.component';
import { MiniLoaderComponent } from '../../../Components/Loader/Mini/Loader-mini.component';

@Component({
  selector: 'app-front-folder-filter',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, FieldComponent, MiniLoaderComponent],
  templateUrl: './Folder-filter.component.html'
})
export class FrontFolderFilterComponent {
  @Input() loadingSearch = false;
  @Input() loadingReset = false;

  filter = output<FolderFilter>({ alias: 'filter' });
  form: FormGroup;

  folderStatuses = [
    { id: 1, label: 'FrontDossiersSection.StatusInProgress' },
    { id: 2, label: 'FrontDossiersSection.StatusWaitingQuote' },
    { id: 3, label: 'FrontDossiersSection.StatusWaitingPayment' },
    { id: 4, label: 'FrontDossiersSection.StatusCompleted' },
    { id: 5, label: 'FrontDossiersSection.StatusCancelled' }
  ];

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      accommodationName: [''],
      ownerLastName: [''],
      folderStatus: [null]
    });
  }

  search(): void {
    const filters = { ...this.form.value };
    this.filter.emit(filters);
  }

  reset(): void {
    this.form.reset();
    this.filter.emit({ reset: true });
  }
}
