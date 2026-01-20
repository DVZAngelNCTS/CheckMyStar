import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-popup',
  standalone: true,
  templateUrl: './Popup.component.html',
  styleUrls: ['./Popup.component.css'],
  imports: [CommonModule, ReactiveFormsModule]
})
export class PopupComponent {

  @Input() visible = false;
  @Input() title = '';
  @Input() confirmLabel = '';
  @Input() cancelLabel = '';

  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onConfirm() {
    this.confirm.emit();
  }

  onCancel() {
    this.cancel.emit();
  }
}
