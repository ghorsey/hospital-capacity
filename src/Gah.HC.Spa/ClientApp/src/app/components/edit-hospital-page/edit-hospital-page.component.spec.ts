import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditHospitalPageComponent } from './edit-hospital-page.component';

describe('EditHospitalPageComponent', () => {
  let component: EditHospitalPageComponent;
  let fixture: ComponentFixture<EditHospitalPageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditHospitalPageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditHospitalPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
