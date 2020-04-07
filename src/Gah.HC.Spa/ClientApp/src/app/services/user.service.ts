import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { User } from './models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private errorHandler: ErrorHandlerService) {}

  createUser(user: User): Observable<any> {
    return this.http
    .post<any>('/api/authorization/register/region', user)
    .pipe(catchError(this.errorHandler.handleError('createUser', [])));
  }
}
