import { Component, OnInit } from '@angular/core';
import { VersionzService } from '../../services/versionz.service';

@Component({
  selector: 'app-about-page',
  templateUrl: './about-page.component.html',
  styleUrls: ['./about-page.component.css']
})
export class AboutPageComponent implements OnInit {
  public version: string;
  constructor(private versionService: VersionzService) { }

  ngOnInit(): void {

    this.versionService.getVersionz().subscribe(r => this.version = r.value);
  }

}
