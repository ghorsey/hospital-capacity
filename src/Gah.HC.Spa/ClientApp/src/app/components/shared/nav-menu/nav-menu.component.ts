import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthenticationService } from '../../../services/authentication.service';
import { Subscription } from 'rxjs';
import { LoaderService } from 'src/app/services/loader.service';
import { ILoaderState } from 'src/app/interfaces/loader-state.interface';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css'],
})
export class NavMenuComponent implements OnInit, OnDestroy {
  public isCollapsed = true;
  public isLoggedOn = false;
  public showSpinner = false;
  private subscription: Subscription;
  private siginChanged$: Subscription;

  constructor(private authenticationService: AuthenticationService, private loaderService: LoaderService) {}

  public ngOnInit() {
    this.isLoggedOn = this.authenticationService.isLoggedOn();
    this.siginChanged$ = this.authenticationService.signInChangedEvent.subscribe(
      (isLoggedOn: boolean) => {
        this.isLoggedOn = isLoggedOn;
      }
    );
    this.subscription = this.loaderService.loaderState.subscribe(
      (state: ILoaderState) => {
        this.showSpinner = state.show;
      },
    );
  }

  public ngOnDestroy() {
    this.siginChanged$.unsubscribe();
    this.subscription.unsubscribe();
  }
}
