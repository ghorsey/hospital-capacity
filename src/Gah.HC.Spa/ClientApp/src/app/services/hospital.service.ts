import { UserDto } from './models/user-dto';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ErrorHandlerService } from './error-handler.service';
import { Observable } from 'rxjs';
import { Result } from './models/result';
import { catchError } from 'rxjs/operators';
import { Hospital } from './models/hospital.model';
import { AuthenticationService } from './authentication.service';
import { HospitalCapacity } from './models/hospital-capacity.model';

@Injectable({
  providedIn: 'root',
})
export class HospitalService {
  private userInfo: UserDto;

  constructor(private http: HttpClient, private errorHandler: ErrorHandlerService, private authenticationService: AuthenticationService,) {
    this.userInfo = this.authenticationService.loggedOnUser();
  }

  getAllHospitals(): Observable<Result<Hospital[]>> {
    return this.http.get<any>(`/api/hospitals`).pipe(catchError(this.errorHandler.handleError('getAllHospitals', [])));
  }

  createHospital(hospital: Hospital): Observable<Result<Hospital>> {
    hospital.createdOn = new Date(new Date().toUTCString()).toISOString();
    hospital.regionId = this.userInfo.regionId;
    delete hospital.slug;
    return this.http.post<any>(`/api/hospitals`, hospital).pipe(catchError(this.errorHandler.handleError('createHospital', [])));
  }

  updateHospital(slug: string, hospital: Hospital): Observable<Result<Hospital>> {
    hospital.updatedOn = new Date(new Date().toUTCString()).toISOString();
    hospital.regionId = this.userInfo.regionId;
    delete hospital.slug;
    delete hospital.region;
    delete hospital.id;
    return this.http.put<any>(`/api/hospitals/${slug}`, hospital).pipe(catchError(this.errorHandler.handleError('updateHospital', [])));
  }

  getHospital(slug: string): Observable<Result<Hospital>> {
    return this.http.get<any>(`/api/hospitals/${slug}`).pipe(catchError(this.errorHandler.handleError('getHospital', [])));
  }

  rapidUpdateHospital(hospital: Hospital): Observable<Result<Hospital>> {
    const payload = {
      isCovid: hospital.isCovid,
      regionId: this.userInfo.regionId,
      updatedOn: new Date(new Date().toUTCString()).toISOString(),
      bedsInUse: hospital.bedsInUse,
      bedCapacity: hospital.bedCapacity
    };
    return this.http.post<any>(`/api/hospitals/${hospital.slug}/rapid-update`, payload).pipe(catchError(this.errorHandler.handleError('rapidUpdateHospital', [])));
  }

  recentCapacity(slug: string): Observable<Result<Array<HospitalCapacity>>> {
    return this.http.get<any>(
      `api/hospitals/${slug}/recent-capacity`)
      .pipe(catchError(this.errorHandler.handleError('getRecentCapacity', [])));
  }
}
