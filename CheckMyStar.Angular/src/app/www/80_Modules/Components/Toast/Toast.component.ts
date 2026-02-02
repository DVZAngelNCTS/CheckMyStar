import { Component, OnInit, OnDestroy, ViewEncapsulation } from '@angular/core';
import { Subscription } from 'rxjs';
import { ToastService } from '../../../90_Services/Toast/Toast.service';
import { Toast } from './Toast.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-toast',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './Toast.component.html',
  styleUrls: ['./Toast.component.css'],
  encapsulation: ViewEncapsulation.None // ← IMPORTANT pour que le CSS s'applique
})
export class ToastComponent implements OnInit, OnDestroy {
  toasts: Toast[] = [];
  private subscription?: Subscription;

  constructor(private toastService: ToastService) {}

  ngOnInit() {
    this.subscription = this.toastService.toasts$.subscribe(
      toasts => this.toasts = toasts
    );
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }

  trackById(_: number, toast: Toast) {
    return toast.id;
  }
}