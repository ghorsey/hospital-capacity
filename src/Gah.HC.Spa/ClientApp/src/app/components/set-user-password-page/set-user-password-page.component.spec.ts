import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SetUserPasswordPageComponent } from './set-user-password-page.component';

describe('SetUserPasswordPageComponent', () => {
  let component: SetUserPasswordPageComponent;
  let fixture: ComponentFixture<SetUserPasswordPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SetUserPasswordPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SetUserPasswordPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
