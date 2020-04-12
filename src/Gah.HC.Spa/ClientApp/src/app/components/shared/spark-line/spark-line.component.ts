import { Component, OnInit, Input } from '@angular/core';
import { Hospital } from '../../../services/models/hospital.model';
import { GoogleChartInterface } from 'ng2-google-charts';

@Component({
  selector: 'app-spark-line',
  templateUrl: './spark-line.component.html',
  styleUrls: ['./spark-line.component.css']
})
export class SparkLineComponent implements OnInit {

  @Input() public hospital: Hospital;
  @Input() public width = 100;
  @Input() public height = 20;

  public sparkLineData: GoogleChartInterface = {
    chartType: 'AreaChart',
    options: {
      chartArea: {width: '100%', height: '100%'}, 
      legend: { position: 'none' },
      backgroundColor: 'transparent',
      colors: [
        'red',
        'blue'
      ],
      hAxis: {
        gridlines: { color: 'transparent'}
      },
      vAxis: {
        gridlines: { color: 'transparent' },
        viewWindow: { min: 0 }
      },
      tooltip: { trigger: 'none' }
    }
  }

  constructor() { }

  ngOnInit(): void {
    if (this.hospital == null) {
      throw new Error("Hospital is required");
    }
    console.log(this.hospital);
    this.sparkLineData.dataTable = [
      ['X', 'Available', 'Occupied'],
      [1, this.hospital.capacity1 - this.hospital.used1, this.hospital.used1],
      [2, this.hospital.capacity2 - this.hospital.used2, this.hospital.used2],
      [3, this.hospital.capacity3 - this.hospital.used3, this.hospital.used3],
      [4, this.hospital.capacity4 - this.hospital.used4, this.hospital.used4],
      [5, this.hospital.capacity5 - this.hospital.used5, this.hospital.used5],
      [6, this.hospital.capacity6 - this.hospital.used6, this.hospital.used6],
      [7, this.hospital.capacity7 - this.hospital.used7, this.hospital.used7],
      [8, this.hospital.capacity8 - this.hospital.used8, this.hospital.used8],
      [9, this.hospital.capacity9 - this.hospital.used9, this.hospital.used9],
      [10, this.hospital.capacity10 - this.hospital.used10, this.hospital.used10]
    ];

    this.sparkLineData.options.height = this.height;
    this.sparkLineData.options.width = this.width;
  }
}
