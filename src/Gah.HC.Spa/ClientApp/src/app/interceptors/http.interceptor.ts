import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
} from '@angular/common/http';

import { Observable } from 'rxjs';
import { finalize } from 'rxjs/internal/operators/finalize';
import { LoaderService } from '../services/loader.service';

/** Pass untouched request through to the next request handler. */
@Injectable()
export class NoopInterceptor implements HttpInterceptor {
  constructor(private loaderService: LoaderService) {}

  intercept(
    req: HttpRequest<any>,
    next: HttpHandler,
  ): Observable<HttpEvent<any>> {
    this.loaderService.show();
    return next.handle(req).pipe(
      finalize(() => {
        this.loaderService.hide();
      }),
    );
  }
}
