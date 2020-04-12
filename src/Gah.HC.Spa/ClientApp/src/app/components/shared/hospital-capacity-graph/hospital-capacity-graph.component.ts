import { Component, OnInit, Input } from '@angular/core';
import { Hospital } from '../../../services/models/hospital.model';
import { GoogleChartInterface } from 'ng2-google-charts';

@Component({
  selector: 'app-hospital-capacity-graph',
  templateUrl: './hospital-capacity-graph.component.html',
  styleUrls: ['./hospital-capacity-graph.component.css']
})
export class HospitalCapacityGraphComponent implements OnInit {
  public capacityData: GoogleChartInterface;

  @Input() public hospital: Hospital;

  constructor() { }

  ngOnInit(): void {
    if (this.hospital == null) {
      throw new Error("Hospital must be set");
    }

    console.log(this.hospital);
  }

}
