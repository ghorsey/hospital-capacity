import { AuthenticationService } from 'src/app/services/authentication.service';
import { USER_TYPE } from './../../constants/common.constants';
import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { UserDto } from 'src/app/services/models/user-dto';
import { Observable } from 'rxjs';
import { UserListItem } from 'src/app/services/models/user-list-item.model';
import { FormControl } from '@angular/forms';
import { Result } from 'src/app/services/models/result';
import { startWith, map } from 'rxjs/operators';

@Component({
  selector: 'app-users-page',
  templateUrl: './users-page.component.html',
  styleUrls: ['./users-page.component.css'],
})
export class UsersPageComponent implements OnInit {
  users$: Observable<UserListItem[]>;
  users: UserListItem[] = [];
  filter = new FormControl('');
  showError = false;
  isEdit = {};
  userInfo: UserDto;

  constructor(private userService: UserService, private authenticationService: AuthenticationService) {
    this.userInfo = this.authenticationService.loggedOnUser();
  }

  private search(text: string): UserListItem[] {
    return this.users.filter((user: UserListItem) => {
      const term = text.toLowerCase();
      return (
        user.userName.toLowerCase().includes(term) ||
        user.userType.toString().includes(term) ||
        user.regionName.toLowerCase().includes(term) ||
        user.hospitalName.toLowerCase().includes(term) ||
        (user.isApproved ? 'Yes' : 'No').toLowerCase().includes(term)
      );
    });
  }

  approve(user: UserListItem, key: string, index: number): void {
    this.userService.approveUser(user.id, user.isApproved).subscribe(
      (result: Result<UserListItem>) => {
        if (result.success) {
          this.isEdit[key] = false;
          this.users[index] = result.value;
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
    if (this.userInfo.userType === USER_TYPE.ADMIN) {
      this.userService.getAllUsers().subscribe(
        (response: Result<UserListItem[]>) => {
          if (response.success) {
            this.users = response.value;
            this.users$ = this.filter.valueChanges.pipe(
              startWith(''),
              map((text) => this.search(text)),
            );
          } else {
            this.showError = true;
          }
        },
        () => {
          this.showError = true;
        },
      );
    } else if (this.userInfo.userType === USER_TYPE.REGION) {
      this.userService.getAllRegionUsers(this.userInfo.regionId).subscribe(
        (response: Result<UserListItem[]>) => {
          if (response.success) {
            this.users = response.value;
            this.users$ = this.filter.valueChanges.pipe(
              startWith(''),
              map((text) => this.search(text)),
            );
          } else {
            this.showError = true;
          }
        },
        () => {
          this.showError = true;
        },
      );
    } else {
      this.userService.getAllHospitalUsers(this.userInfo.hospitalId).subscribe(
        (response: Result<UserListItem[]>) => {
          if (response.success) {
            this.users = response.value;
            this.users$ = this.filter.valueChanges.pipe(
              startWith(''),
              map((text) => this.search(text)),
            );
          } else {
            this.showError = true;
          }
        },
        () => {
          this.showError = true;
        },
      );
    }
  }
}
