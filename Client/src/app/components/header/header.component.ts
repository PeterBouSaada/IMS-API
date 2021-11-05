import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  route : string;

  constructor(private _router: Router) {
    this.getRoute();
    this._router.events.subscribe(() => this.getRoute());
  }

  ngOnInit(): void {
  }

  getRoute()
  {
    this.route = this._router.url;
  }

}
