import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { UserModel } from '../../../20_Models/Common/User.model';
import { FieldComponent } from '../Field/Field.component';
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
  selector: 'app-owner-autocomplete',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FieldComponent, TranslationModule],
  templateUrl: './Owner-autocompletion.component.html',
  styleUrls: ['./Owner-autocompletion.component.css']
})
export class OwnerAutocompleteComponent implements OnChanges {
  @Input() users: UserModel[] = [];
  @Input() selectedIdentifier: number | null = null;
  @Input() placeholderLabelKey: string = 'DossiersSection.Owner';
  @Output() ownerSelected = new EventEmitter<number>();

  searchControl = new FormControl('', { nonNullable: true });
  suggestions: UserModel[] = [];

  constructor() {
    this.searchControl.valueChanges.subscribe(value => {
      this.filterUsers(value ?? '');
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['selectedIdentifier'] || changes['users']) {
      this.syncSelectedUserLabel();
      this.filterUsers(this.searchControl.value);
    }
  }

  onFocus(): void {
    this.filterUsers(this.searchControl.value);
  }

  onBlur(): void {
    // Delay so click on suggestion is still captured before closing.
    setTimeout(() => {
      this.suggestions = [];
    }, 150);
  }

  selectOwner(owner: UserModel): void {
    this.ownerSelected.emit(owner.identifier);
    this.searchControl.setValue(this.formatOwnerLabel(owner), { emitEvent: false });
    this.suggestions = [];
  }

  private filterUsers(rawTerm: string): void {
    const term = rawTerm.trim().toLowerCase();

    if (!term) {
      this.suggestions = this.users.slice(0, 25);
      return;
    }

    this.suggestions = this.users
      .filter(user => this.toSearchableText(user).includes(term))
      .slice(0, 25);
  }

  private syncSelectedUserLabel(): void {
    if (!this.selectedIdentifier) {
      return;
    }

    const selectedUser = this.users.find(u => u.identifier === this.selectedIdentifier);
    if (!selectedUser) {
      return;
    }

    this.searchControl.setValue(this.formatOwnerLabel(selectedUser), { emitEvent: false });
  }

  private formatOwnerLabel(owner: UserModel): string {
    const fullName = [owner.lastName, owner.firstName].filter(Boolean).join(' ').trim();
    if (owner.email) {
      return `${fullName} (${owner.email})`;
    }
    return fullName;
  }

  private toSearchableText(owner: UserModel): string {
    return [
      owner.identifier,
      owner.lastName,
      owner.firstName,
      owner.email,
      owner.phone
    ]
      .filter(Boolean)
      .join(' ')
      .toLowerCase();
  }
}