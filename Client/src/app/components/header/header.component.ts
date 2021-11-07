import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { EventService } from 'src/app/services/Event/event.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  route : string;
  @Input() searchQuery: string;

  constructor(private _router: Router, private _eventService: EventService) {
    this.getRoute();
    this._router.events.subscribe(() => this.getRoute());
  }

  ngOnInit(): void {
  }

  searchClicked()
  {
    this._eventService.setSearch(this.searchQuery);
  }

  getRoute()
  {
    this.route = this._router.url;
  }

}
