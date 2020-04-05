import { Injectable } from '@angular/core';
import { ToastMessage } from './models/toast-message';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  public toasts: ToastMessage[] = [];

  public show(toast: ToastMessage) {
    this.toasts.push(toast);
  }

  public remove(toast: ToastMessage) {
    this.toasts = this.toasts.filter(t => t !== toast);
  }
}

