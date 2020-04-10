import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';
import { ILoaderState } from '../interfaces/loader-state.interface';
import { ILoaderService } from '../interfaces/loader-service.interface';

@Injectable({
  providedIn: 'root',
})
export class LoaderService implements ILoaderService {
  private loaderSubject = new Subject<ILoaderState>();
  loaderState = this.loaderSubject.asObservable();
  constructor() {}
  show() {
    this.loaderSubject.next(<ILoaderState>{ show: true });
  }
  hide() {
    this.loaderSubject.next(<ILoaderState>{ show: false });
  }
}
