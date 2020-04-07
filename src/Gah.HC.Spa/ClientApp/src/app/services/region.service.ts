import { Region } from './models/region.model';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ErrorHandlerService } from './error-handler.service';
import { Observable } from 'rxjs';
import { Result } from './models/result';

@Injectable({
  providedIn: 'root',
})
export class RegionService {
  constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {}

  findRegion(name: string): Observable<Result<Region>> {
    return this.http.get<any>(`/api/regions?name=${name}`).pipe(catchError(this.errorHandler.handleError('findRegion', [])));
  }
}
