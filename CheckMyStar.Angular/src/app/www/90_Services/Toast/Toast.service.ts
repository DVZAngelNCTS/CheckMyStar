import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Toast, ToastType } from '../../80_Modules/Components/Toast/Toast.model';

@Injectable({ providedIn: 'root' })
export class ToastService {
  private toastsSubject = new BehaviorSubject<Toast[]>([]);
  toasts$ = this.toastsSubject.asObservable();

  private counter = 0;

  show(message: string, type: ToastType = 'info', duration = 3000) {
    const toast: Toast = {
      id: ++this.counter,
      message,
      type,
      duration
    };

    const toasts = [...this.toastsSubject.value, toast];
    console.log('Toasts actuels:', toasts);
    this.toastsSubject.next(toasts);

    setTimeout(() => this.remove(toast.id), duration);
  }

  remove(id: number) {
    this.toastsSubject.next(
      this.toastsSubject.value.filter(t => t.id !== id)
    );
  }
}
