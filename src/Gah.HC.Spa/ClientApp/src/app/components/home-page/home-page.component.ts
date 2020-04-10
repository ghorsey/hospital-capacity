import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Hospital } from 'src/app/services/models/hospital.model';
import { FormControl } from '@angular/forms';
import { HospitalService } from 'src/app/services/hospital.service';
import { Result } from 'src/app/services/models/result';
import { startWith, map } from 'rxjs/operators';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  hospitals$: Observable<Hospital[]>;
  hospitals: Hospital[] = [];
  filter = new FormControl('');
  showError = false;

  constructor(private hospitalService: HospitalService) { }

  private search(text: string): Hospital[] {
    return this.hospitals.filter((hospital) => {
      const term = text.toLowerCase();
      return (
        hospital.regionName.toLowerCase().includes(term) ||
        hospital.name.toLowerCase().includes(term) ||
        hospital.address1.toLowerCase().includes(term) ||
        hospital.address2.toLowerCase().includes(term) ||
        hospital.city.toLowerCase().includes(term) ||
        hospital.state.toLowerCase().includes(term) ||
        hospital.postalCode.toLowerCase().includes(term) ||
        hospital.phone.toLowerCase().includes(term) ||
        (hospital.isCovid ? 'Yes' : 'No').toLowerCase().includes(term) ||
        hospital.bedsInUse.toString().toLowerCase().includes(term) ||
        hospital.bedCapacity.toString().toLowerCase().includes(term) ||
        hospital.percentOfUsage.toString().toLowerCase().includes(term) ||
        hospital.updatedOn.toString().toLowerCase().includes(term)
      );
    });
  }

  ngOnInit(): void {
    this.hospitalService.getAllHospitals().subscribe(
      (response: Result<Hospital[]>) => {
        if (response.success) {
          this.hospitals = response.value;
          this.hospitals$ = this.filter.valueChanges.pipe(
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
