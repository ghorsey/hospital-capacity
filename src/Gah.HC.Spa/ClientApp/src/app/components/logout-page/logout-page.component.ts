import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-logout-page',
  template: ''
})
export class LogoutPageComponent implements OnInit {

  constructor(
    private authorizationService: AuthenticationService,
    private router: Router) { }

  ngOnInit(): void {
    this.authorizationService.logout().subscribe(() => {
      this.router.navigate(['']);
    });
  }

}
