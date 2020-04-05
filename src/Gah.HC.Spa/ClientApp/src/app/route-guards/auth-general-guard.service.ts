import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';
import { AppUserType } from '../services/models/app-user-type.enum';

@Injectable({
  providedIn: 'root'
})
export class AuthGeneralGuardService implements CanActivate {

  constructor(private authService: AuthenticationService, private router: Router) {}
  canActivate(
    route: import("@angular/router").ActivatedRouteSnapshot,
    state: import("@angular/router").RouterStateSnapshot): boolean | import("@angular/router").UrlTree | import("rxjs").Observable<boolean | import("@angular/router").UrlTree> | Promise<boolean | import("@angular/router").UrlTree> {
      console.log(route);
      console.log(state);

    if (this.authService.isLoggedOn()) {
      return true;
    }

    return this.router.navigate(['/login']);
    }
}
