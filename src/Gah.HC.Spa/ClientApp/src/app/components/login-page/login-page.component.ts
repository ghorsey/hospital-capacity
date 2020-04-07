import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';
import { Result } from '../../services/models/result';
import { LoginInput } from '../../services/models/login-input';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css'],
})
export class LoginPageComponent {
  submitted = false;
  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    rememberMe: new FormControl(false),
  });

  constructor(public title: Title, private router: Router, private authService: AuthenticationService) {
    title.setTitle('Login: Hospital Capacity');
  }

  public login(): void {
    this.submitted = true;
    if (this.loginForm.valid) {
      this.authService.authenticate(this.loginForm.value as LoginInput).subscribe(
        () => {
          this.authService.me().subscribe((me) => {
            this.authService.login(me.value);
            this.router.navigate(['/dashboard']);
          });
        },
        (e) => {
          const error = e.error as Result<string>;
          console.error(error);
        },
      );
    }
  }
}
