import { TestBed } from '@angular/core/testing';

import { ToastService } from './toasts.service';
import { ToastMessage } from '../models/toast-message';

describe('AppToastServicet', () => {
  let service: ToastService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ToastService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should add and remove a toast messsage', () => {
    const toast = new ToastMessage('header', 'body');
    service.show(toast);

    expect(service.toasts.length).toEqual(1);

    service.remove(toast);

    expect(service.toasts.length).toEqual(0);
  });
});
