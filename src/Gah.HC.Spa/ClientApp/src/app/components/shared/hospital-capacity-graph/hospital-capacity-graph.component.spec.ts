import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HospitalCapacityGraphComponent } from './hospital-capacity-graph.component';

describe('HospitalCapacityGraphComponent', () => {
  let component: HospitalCapacityGraphComponent;
  let fixture: ComponentFixture<HospitalCapacityGraphComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HospitalCapacityGraphComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HospitalCapacityGraphComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
