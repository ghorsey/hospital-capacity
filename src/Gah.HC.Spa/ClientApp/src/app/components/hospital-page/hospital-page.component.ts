import { Hospital } from 'src/app/services/models/hospital.model';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { HospitalService } from 'src/app/services/hospital.service';
import { ActivatedRoute } from '@angular/router';
import { Result } from 'src/app/services/models/result';
import { STATES } from 'src/app/constants/common.constants';

@Component({
  selector: 'app-hospital-page',
  templateUrl: './hospital-page.component.html',
  styleUrls: ['./hospital-page.component.css'],
})
export class HospitalPageComponent implements OnInit {
  hospitalForm: FormGroup;
  submitted = false;
  hospital: Hospital;
  showError = false;
  hospitalId = '';
  edit = false;
  states: string[] = STATES;

  constructor(private route: ActivatedRoute, private hospitalService: HospitalService) {}

  private createControls(): void {
    this.hospitalForm = new FormGroup({
      name: new FormControl(this.hospital.name, [Validators.required]),
      address1: new FormControl(this.hospital.address1, [Validators.required]),
      address2: new FormControl(this.hospital.address2, []),
      city: new FormControl(this.hospital.city, [Validators.required]),
      state: new FormControl(this.hospital.state, [Validators.required]),
      postalCode: new FormControl(this.hospital.postalCode, [Validators.required]),
      phone: new FormControl(this.hospital.phone, []),
      isCovid: new FormControl(this.hospital.isCovid, [Validators.required]),
      bedCapacity: new FormControl(this.hospital.bedCapacity, [Validators.required]),
      bedsInUse: new FormControl(this.hospital.bedsInUse, [Validators.required]),
    });
  }

  private setHospital(): void {
    this.hospital.name = this.hospitalForm.controls.name.value;
    this.hospital.address1 = this.hospitalForm.controls.address1.value;
    this.hospital.address2 = this.hospitalForm.controls.address2.value;
    this.hospital.city = this.hospitalForm.controls.city.value;
    this.hospital.state = this.hospitalForm.controls.state.value;
    this.hospital.postalCode = this.hospitalForm.controls.postalCode.value;
    this.hospital.phone = this.hospitalForm.controls.phone.value;
    this.hospital.isCovid = this.hospitalForm.controls.isCovid.value;
    this.hospital.bedCapacity = this.hospitalForm.controls.bedCapacity.value;
    this.hospital.bedsInUse = this.hospitalForm.controls.bedsInUse.value;
  }

  private setControls(): void {
    this.hospitalForm.controls.name.setValue(this.hospital.name);
    this.hospitalForm.controls.address1.setValue(this.hospital.address1);
    this.hospitalForm.controls.address2.setValue(this.hospital.address2);
    this.hospitalForm.controls.city.setValue(this.hospital.city);
    this.hospitalForm.controls.state.setValue(this.hospital.state);
    this.hospitalForm.controls.postalCode.setValue(this.hospital.postalCode);
    this.hospitalForm.controls.phone.setValue(this.hospital.phone);
    this.hospitalForm.controls.isCovid.setValue(this.hospital.isCovid);
    this.hospitalForm.controls.bedCapacity.setValue(this.hospital.bedCapacity);
    this.hospitalForm.controls.bedsInUse.setValue(this.hospital.bedsInUse);
  }

  private createHospital(): void {
    this.hospitalService.createHospital(this.hospital).subscribe(
      (response: Result<Hospital>) => {
        if (response.success) {
          this.edit = true;
          this.hospital = <Hospital>response.value;
          this.hospitalId = this.hospital.slug;
        } else {
          this.showError = true;
        }
      },
      () => {
        this.showError = true;
      },
    );
  }

  private updateHospital(): void {
    this.hospitalService.updateHospital(this.hospitalId, this.hospital).subscribe(
      (response: Result<Hospital>) => {
        if (response.success) {
        } else {
          this.showError = true;
        }
      },
      () => {
        this.showError = true;
      },
    );
  }

  private getHospital(): void {
    this.hospitalService.getHospital(this.hospitalId).subscribe(
      (response: Result<Hospital>) => {
        if (response.success) {
          this.hospital = <Hospital>response.value;
          this.setControls();
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
    this.edit = this.route.snapshot.paramMap.get('id') !== 'new';
    this.hospitalId = this.route.snapshot.paramMap.get('id');
    if (this.edit) {
      this.hospital = new Hospital(this.hospitalId);
      this.getHospital();
    } else {
      this.hospital = new Hospital();
    }
    this.createControls();
  }

  submit(): void {
    this.submitted = true;
    if (this.hospitalForm.valid) {
      this.setHospital();
      if (this.edit) {
        this.updateHospital();
      } else {
        this.createHospital();
      }
    }
  }
}
