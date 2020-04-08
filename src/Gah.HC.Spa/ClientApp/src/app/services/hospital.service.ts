import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ErrorHandlerService } from './error-handler.service';
import { Observable } from 'rxjs';
import { Result } from './models/result';
import { catchError } from 'rxjs/operators';
import { Hospital } from './models/hospital.model';

@Injectable({
  providedIn: 'root'
})
export class HospitalService {

  constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {}

  getAllHospitals(): Observable<Result<Hospital[]>> {
    return this.http.get<any>(`/api/hospitals`).pipe(catchError(this.errorHandler.handleError('getAllHospitals', [])));
  }
}
