import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/services/models/user.model';
import { CustomValidators } from 'src/app/validators/custom.validators';
import { RegionService } from 'src/app/services/region.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Result } from 'src/app/services/models/result';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration-page',
  templateUrl: './registration-page.component.html',
  styleUrls: ['./registration-page.component.css'],
})
export class RegistrationPageComponent implements OnInit {
  registrationForm: FormGroup;
  submitted = false;
  user: User;
  showError = false;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private regionService: RegionService,
    private authService: AuthenticationService,
    private router: Router,
  ) {}

  private createControls(): void {
    this.registrationForm = this.formBuilder.group(
      {
        email: [this.user.email, [Validators.required, Validators.email]],
        regionName: [this.user.regionName, [Validators.required]],
        password: [this.user.password, [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{6,}')]],
        confirmPassword: [this.user.confirmPassword, [Validators.required]],
      },
      {
        validator: CustomValidators.Match('password', 'confirmPassword'),
      },
    );
  }

  private setUser(): void {
    this.user.email = this.registrationForm.controls.email.value;
    this.user.regionName = this.registrationForm.controls.regionName.value;
    this.user.password = this.registrationForm.controls.password.value;
    this.user.confirmPassword = this.registrationForm.controls.confirmPassword.value;
  }

  private login(): void {
    this.authService.authenticate({ email: this.user.email, password: this.user.password, rememberMe: false }).subscribe(
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

  private checkRegion(): void {
    const region = this.registrationForm.get('regionName').value;
    this.regionService.findRegion(region).subscribe(
      (response: any) => {
        if (response.success) {
          if (response.value.length > 0) {
            this.registrationForm.get('regionName').setErrors({ regionExist: true });
          } else {
            this.setUser();
            this.createUser();
          }
        } else {
          this.showError = true;
        }
      },
      () => {
        this.showError = true;
      },
    );
  }

  private createUser(): void {
    this.userService.createUser(this.user).subscribe(
      (response: any) => {
        if (response.success) {
          this.login();
        } else {
          this.showError = true;
        }
      },
      () => {
        this.showError = true;
      },
    );
  }

  ngOnInit(): void {
    this.user = new User();
    this.createControls();
  }

  submit(): void {
    this.submitted = true;
    if (this.registrationForm.valid) {
      this.checkRegion();
    }
  }
}
