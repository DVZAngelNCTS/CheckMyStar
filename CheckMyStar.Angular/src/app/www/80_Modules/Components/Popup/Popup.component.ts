import { Component, input, Input, Output, EventEmitter, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { MiniLoaderComponent } from '../../Components/Loader/Mini/Loader-mini.component';

@Component({
  selector: 'app-popup',
  standalone: true,
  templateUrl: './Popup.component.html',
  styleUrls: ['./Popup.component.css'],
  imports: [CommonModule, ReactiveFormsModule, MiniLoaderComponent],
  encapsulation: ViewEncapsulation.None
})
export class PopupComponent {
  icon = input<string>('');

  @Input() loading = false;
  
  @Input() visible = false;
  @Input() title = '';
  @Input() confirmLabel = '';
  @Input() cancelLabel = '';
  @Input() errorMessage: string | null = null;
  @Input() large = false;
  
  @Output() confirm = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  onConfirm() {
    this.confirm.emit();
  }

  onCancel() {
    this.cancel.emit();
  }
}
