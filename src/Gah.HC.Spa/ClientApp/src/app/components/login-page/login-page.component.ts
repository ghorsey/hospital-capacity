import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { ToastService } from '../../services/toasts.service';
import { ToastMessage } from '../../services/models/toast-message';
import { AuthenticationService } from '../../services/authentication.service';
import { Result } from '../../services/models/result';
import { LoginInput } from '../../services/models/login-input';
import { SessionStorageService } from 'ngx-webstorage'

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent {
  loginForm = new FormGroup({
    'email': new FormControl('', [Validators.required, Validators.email]),
    'password': new FormControl('', [Validators.required]),
    'rememberMe': new FormControl(false)
  });

  public isSubmitted = false;
  public get formControls() { return this.loginForm.controls; }

  constructor(
    public title: Title,
    private toastService: ToastService,
    private sessionStorageService: SessionStorageService,
    private authService: AuthenticationService) {
    title.setTitle("Login: Hospital Capacity");
  }

  public login(): void {
    console.log(this.loginForm.value);
    this.isSubmitted = true;
    if (this.loginForm.invalid) {

      if (this.formControls.email.errors?.email) {
        this.toastService.show(new ToastMessage("Invalid email format", "bg-danger text-light"));
      }

      if (this.formControls.email.errors?.required) {
        this.toastService.show(new ToastMessage("Email is required", "bg-danger text-light"));
      }

      if (this.formControls.password.errors?.required) {
        this.toastService.show(new ToastMessage("Password is required", "bg-danger text-light"));
      }

      return;
    }

    this.authService.login(this.loginForm.value as LoginInput).subscribe(r => {
      this.authService.me().subscribe(me => {
        console.log(me);
        this.sessionStorageService.store("me", me.value);
      })
    }, e => {
        const error = e.error as Result<string>;
        this.toastService.show(new ToastMessage(error.message, "bg-danger text-light"));
    });
    console.log("log in the user");
  }
}
