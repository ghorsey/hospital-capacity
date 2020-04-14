import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { User } from './models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {}

  createUser(user: User): Observable<any> {
    return this.http
      .post<any>('/api/authorization/register/region', user)
      .pipe(catchError(this.errorHandler.handleError('createUser', [])));
  }

  getAllUsers() {
    return this.http.get<any>(`api/users`).pipe(catchError(this.errorHandler.handleError('getAllUsers', [])));
  }

  getAllRegionUsers(slug: string) {
    return this.http.get<any>(`api/regions/${slug}/users`).pipe(catchError(this.errorHandler.handleError('getAllRegionUsers', [])));
  }

  getAllHospitalUsers(slug: string) {
    return this.http.get<any>(`api/hospitals/${slug}/users`).pipe(catchError(this.errorHandler.handleError('getAllHospitalUsers', [])));
  }

  setUserPassword(id: string, newPassword: string) {
    return this.http
      .post<any>(`/api​/users​/${id}​/set-password`, { newPassword })
      .pipe(catchError(this.errorHandler.handleError('setUserPassword', [])));
  }

  approveUser(id: string, isApproved: boolean) {
    return this.http
      .post<any>(`​/api​/users​/${id}​/set-authorized`, { isApproved })
      .pipe(catchError(this.errorHandler.handleError('approveUser', [])));
  }
}
