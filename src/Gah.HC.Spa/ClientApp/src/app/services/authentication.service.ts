import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginInput } from './models/login-input';
import { Observable } from 'rxjs';
import { UserDto } from './models/user-dto';
import { Result } from './models/result';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient) { }

  public login(auth: LoginInput) {
    return this.http.post("/api/authorization/login", auth);
  }

  public me(): Observable<Result<UserDto>> {
    return this.http.get("/api/authorization/me") as Observable<Result<UserDto>>;
  }
}
