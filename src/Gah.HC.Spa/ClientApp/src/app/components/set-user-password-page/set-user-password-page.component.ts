import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomValidators } from 'src/app/validators/custom.validators';
import { HttpErrorResponse } from '@angular/common/http';
import { Result } from 'src/app/services/models/result';

@Component({
  selector: 'app-set-user-password-page',
  templateUrl: './set-user-password-page.component.html',
  styleUrls: ['./set-user-password-page.component.css'],
})
export class SetUserPasswordPageComponent implements OnInit {
  setPasswordForm: FormGroup;
  submitted = false;
  showError = false;
  showErrorMessage: string;
  userId = '';

  constructor(private userService: UserService, private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router) {}

  private createControls(): void {
    this.setPasswordForm = this.formBuilder.group(
      {
        password: [
          '',
          [Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&.])[A-Za-zd$@$!%*?&].{6,}')],
        ],
        confirmPassword: ['', [Validators.required]],
      },
      {
        validator: CustomValidators.Match('password', 'confirmPassword'),
      },
    );
  }

  private setNewUserPassword(): void {
    this.userService.setUserPassword(this.userId, this.setPasswordForm.get('password').value).subscribe(
      () => {
        this.router.navigate(['/users']);
      },
      (error: HttpErrorResponse) => {
        const e = error.error as Result<string>;
        this.showErrorMessage = e.message;
        this.showError = true;
      },
    );
  }

  ngOnInit(): void {
    this.userId = this.route.snapshot.paramMap.get('id');
    this.createControls();
  }

  submit(): void {
    this.submitted = true;
    if (this.setPasswordForm.valid) {
      this.setNewUserPassword();
    }
  }
}
