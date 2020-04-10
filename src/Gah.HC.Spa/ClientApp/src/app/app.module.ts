import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxWebstorageModule } from 'ngx-webstorage';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/shared/nav-menu/nav-menu.component';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ToastsComponent } from './components/shared/toasts/toasts.component';
import { DashboardPageComponent } from './components/dashboard-page/dashboard-page.component';
import { LogoutPageComponent } from './components/logout-page/logout-page.component';
import { AuthGeneralGuardService } from './route-guards/auth-general-guard.service';
import { RegistrationPageComponent } from './components/registration-page/registration-page.component';
import { AboutPageComponent } from './components/about-page/about-page.component';
import { HospitalPageComponent } from './components/hospital-page/hospital-page.component';
import { NoopInterceptor } from './interceptors/http.interceptor';
import { UsersPageComponent } from './components/users-page/users-page.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomePageComponent,
    LoginPageComponent,
    ToastsComponent,
    DashboardPageComponent,
    LogoutPageComponent,
    RegistrationPageComponent,
    HospitalPageComponent,
    UsersPageComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxWebstorageModule.forRoot(),
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomePageComponent, pathMatch: 'full' },
      { path: 'login', component: LoginPageComponent },
      { path: 'logout', component: LogoutPageComponent },
      { path: 'registration', component: RegistrationPageComponent },
      { path: 'about', component: AboutPageComponent },
      {
        path: 'dashboard',
        component: DashboardPageComponent,
        canActivate: [ AuthGeneralGuardService ]
      },
      {
        path: 'users',
        component: UsersPageComponent,
        canActivate: [ AuthGeneralGuardService ]
      },
      {
        path: 'hospital/:id',
        component: HospitalPageComponent,
        canActivate: [ AuthGeneralGuardService ]
      }
    ])
  ],
  providers: [
    Title,
    { provide: HTTP_INTERCEPTORS, useClass: NoopInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
