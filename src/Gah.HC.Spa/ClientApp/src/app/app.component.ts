import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router, NavigationEnd, ActivatedRoute } from '@angular/router';
import { filter, map } from 'rxjs/operators';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private titleService: Title) { }

  ngOnInit(): void {
    const baseTitle = 'Hospital Capacity';
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => {
        const child = this.activatedRoute.firstChild;
        if (child.snapshot.data['title']) {
          return child.snapshot.data['title'];
        }

        return baseTitle;
      })
    ).subscribe((title: string) => this.titleService.setTitle(title));
  }
  
}
