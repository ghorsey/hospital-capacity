import { BrowserModule, Title } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxWebstorageModule } from 'ngx-webstorage';
import { Ng2GoogleChartsModule, GoogleChartsSettings } from 'ng2-google-charts';

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
import { EditHospitalPageComponent } from './components/edit-hospital-page/edit-hospital-page.component';
import { NoopInterceptor } from './interceptors/http.interceptor';
import { UsersPageComponent } from './components/users-page/users-page.component';
import { SparkLineComponent } from './components/shared/spark-line/spark-line.component';
import { HospitalCapacityGraphComponent } from './components/shared/hospital-capacity-graph/hospital-capacity-graph.component';
import { HospitalPageComponent } from './components/hospital-page/hospital-page.component';

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
    EditHospitalPageComponent,
    UsersPageComponent,
    SparkLineComponent,
    HospitalCapacityGraphComponent,
    HospitalPageComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    Ng2GoogleChartsModule,
    ReactiveFormsModule,
    NgxWebstorageModule.forRoot(),
    NgbModule,
    RouterModule.forRoot([
      { path: '', component: HomePageComponent, pathMatch: 'full', data: { title: 'Hospital Capacity' } },
      { path: 'login', component: LoginPageComponent, data: { title: 'Login: Hospital Capacity' } },
      { path: 'logout', component: LogoutPageComponent, data: { title: 'Logout: Hospital Capacity' } },
      { path: 'registration', component: RegistrationPageComponent, data: { title: 'Registration: Hospital Capacity' } },
      { path: 'about', component: AboutPageComponent, data: { title: 'About: Hospital Capacity' } },
      { path: ':id', component: HospitalPageComponent, data: { title: "Hospital: Hospital Capacity" } },
      {
        path: 'dashboard',
        component: DashboardPageComponent,
        canActivate: [AuthGeneralGuardService],
        data: { title: 'Dashboard: Hospital Capacity'}
      },
      {
        path: 'users',
        component: UsersPageComponent,
        canActivate: [AuthGeneralGuardService],
        data: { title: 'Users: Hospital Capacity' }
      },
      {
        path: 'dashboard/:id',
        component: EditHospitalPageComponent,
        canActivate: [AuthGeneralGuardService],
        data: { title: 'Add/Edit Hospital: Hospital Capacity' }
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
