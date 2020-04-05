import { Injectable, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { LoginInput } from './models/login-input';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { UserDto } from './models/user-dto';
import { Result } from './models/result';
import { SessionStorageService } from 'ngx-webstorage';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  @Output() public signInChangedEvent = new EventEmitter<boolean>();

  private readonly loginKey = "loggedIn";

  constructor(
    private sessionStorage: SessionStorageService,
    private http: HttpClient) { }

  public authenticate(auth: LoginInput) {
    return this.http.post("/api/authorization/login", auth).pipe(map(() => this.signInChangedEvent.emit(true)));
  }

  public me(): Observable<Result<UserDto>> {
    return this.http.get("/api/authorization/me") as Observable<Result<UserDto>>;
  }

  public login(user: UserDto): void {
    this.sessionStorage.store(this.loginKey, user);
  }

  public loggedOnUser(): UserDto {
    return this.sessionStorage.retrieve(this.loginKey) as UserDto;
  }

  public logout() {
    return this.http.post("/api/authorization/logout", null).pipe(map(() => {
      this.sessionStorage.clear(this.loginKey);
      this.signInChangedEvent.emit(false);
    }));
  }

  public isLoggedOn(): boolean {
    return this.loggedOnUser() !== null;
  }

}
