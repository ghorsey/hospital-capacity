import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home-page.component.html',
})
export class HomePageComponent {
  constructor(title: Title) {
    title.setTitle("View Hotel Capacity: Hotel Capacity App")
  }
}
