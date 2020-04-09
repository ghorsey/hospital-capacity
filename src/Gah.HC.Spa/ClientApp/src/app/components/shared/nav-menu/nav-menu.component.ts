import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthenticationService } from '../../../services/authentication.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public isLoggedOn = false;

  private siginChanged$: Subscription;

  constructor(private authenticationService: AuthenticationService) {}

  public ngOnInit() {
    this.isLoggedOn = this.authenticationService.isLoggedOn();
    this.siginChanged$ = this.authenticationService.signInChangedEvent.subscribe(
      (isLoggedOn: boolean) => {
        this.isLoggedOn = isLoggedOn;
      }
    );
  }

  public ngOnDestroy() {
    this.siginChanged$.unsubscribe();
  }
}
