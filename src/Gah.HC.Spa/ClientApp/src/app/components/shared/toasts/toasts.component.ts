import { Component } from '@angular/core';
import { ToastService } from '../../../services/toasts.service';

@Component({
  selector: 'app-toasts',
  templateUrl: './toasts.component.html',
  styleUrls: ['./toasts.component.css']
})
export class ToastsComponent {
  constructor(public toastService: ToastService) { }
}
