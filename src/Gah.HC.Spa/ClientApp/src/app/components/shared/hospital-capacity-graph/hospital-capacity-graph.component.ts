import { Component, OnInit, Input } from '@angular/core';
import { Hospital } from '../../../services/models/hospital.model';
import { GoogleChartInterface } from 'ng2-google-charts';
import { HospitalService } from '../../../services/hospital.service';
import { HospitalCapacity } from '../../../services/models/hospital-capacity.model';

@Component({
  selector: 'app-hospital-capacity-graph',
  templateUrl: './hospital-capacity-graph.component.html',
  styleUrls: ['./hospital-capacity-graph.component.css']
})
export class HospitalCapacityGraphComponent implements OnInit {
  public capacityData: GoogleChartInterface = {
    chartType: 'AreaChart',
    options: {
      //chartArea: {width: '100%', height: '100%'}, 
      //legend: { position: 'none' },
      backgroundColor: 'transparent',
      colors: [
        'red',
        'blue'
      ],
      hAxis: {
        //gridlines: { color: 'transparent'}
        title: 'Time',
        format: 'MMM d, y',
      },
      vAxis: {
        //gridlines: { color: 'transparent' },
        title: 'Beds'
      },
      //height: '100%',
      width: '100%',
      height: 450,
    }
  }

  @Input() public hospital: Hospital;

  constructor(private hospitalService: HospitalService) { }

  private initChartData(capacity: HospitalCapacity[]) {
    var data = [];
    data.push(['Date', 'Available', 'Occupied'])

    for (const item of capacity) {
      data.push([new Date(item.createdOn).toLocaleDateString(), item.bedCapacity - item.bedsInUse, item.bedsInUse]);
    }

    this.capacityData.dataTable = data;
    this.capacityData.component.draw();
  }

  ngOnInit(): void {
    if (this.hospital == null) {
      throw new Error("Hospital must be set");
    }

    this.hospitalService.recentCapacity(this.hospital.slug)
      .subscribe(
        result => this.initChartData(result.value),
        error => console.log(error));
  }

}
