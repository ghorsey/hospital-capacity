import { Component, OnInit } from '@angular/core';
import { Hospital } from '../../services/models/hospital.model';
import { HospitalService } from '../../services/hospital.service';
import { ActivatedRoute } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-hospital-page',
  templateUrl: './hospital-page.component.html',
  styleUrls: ['./hospital-page.component.css'],
})
export class HospitalPageComponent implements OnInit {
  public hospital: Hospital;
  public showError: boolean;
  public showErrorMessage: string;

  constructor(private activatedRoute: ActivatedRoute, private hospitalService: HospitalService) {}

  ngOnInit(): void {
    const slug = this.activatedRoute.snapshot.paramMap.get('id');

    this.hospitalService.getHospital(slug).subscribe(
      (result) => (this.hospital = result.value),
      (error: HttpErrorResponse) => {
        if (error.status === 404) {
          this.showErrorMessage = `Hospital '${slug}' was not found`;
        } else {
          this.showErrorMessage = error.message;
        }
        this.showError = true;
      },
    );
  }
}
