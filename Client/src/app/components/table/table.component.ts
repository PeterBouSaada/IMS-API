import { KeyValue } from '@angular/common';
import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Item } from 'src/app/Models/Item';
import { EventService } from 'src/app/services/Event/event.service';
import { RequestService } from 'src/app/services/request/request.service';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})

export class TableComponent implements OnInit {

  @Input() caption: string;
  @Input() headers: string[];
  @Input() data: any[];
  @Output() viewEvent: EventEmitter<any> = new EventEmitter();
  @Output() editEvent: EventEmitter<any> = new EventEmitter();
  subscription : any;

  constructor(private _eventService: EventService, private _routerService: Router, private _requestService: RequestService) {}

  ngOnInit(): void {
    this.subscription = this._eventService.subject.subscribe((data: string) =>
    {
      this.search(data);
    });
  }

  search(data: string)
  {
    let route = this._routerService.url;
    if(route == "/Inventory")
    {
      if(!(data.length > 1) || data == undefined)
      {
        this._requestService.get("item")?.subscribe(response => {
          this.data = response.body;
        });
      }
      else
      {
        this._requestService.post("item/search", {part_number: data})?.subscribe(response => {
          this.data = response.body;
        });
      }
    }
    else if(route == "/Users")
    {
      if(!(data.length > 1) || data == undefined)
      {
      this._requestService.get("user")?.subscribe(response => {
        this.data = response.body;
      });
      }
      else
      {
        this._requestService.post("user/search", {username: data})?.subscribe(response => {
          this.data = response.body;
        });
      }
    }
  }

  forLimit(num: number) {
    if(num > this.data.length)
    {
      let items: any[] = [];
      let i: number;
      for(i = 0; i < num; i++)
      {
        if (i < this.data.length)
        {
         items[i] = this.data[i];
        }
        else 
        {
          items[i] = new Object;
        }
      }
      return items;
    }
    console.log(this.data);
    return this.data;
  }

  view(id : string)
  {
    this.viewEvent.emit(id);
  }

  edit(id : string)
  {
    this.editEvent.emit(id);
  }

  originalOrder = (a: KeyValue<number,string>, b: KeyValue<number,string>): number => {
    return 0;
  }

  valueAscOrder = (a: KeyValue<number,string>, b: KeyValue<number,string>): number => {
    return a.value.localeCompare(b.value);
  }

  keyDescOrder = (a: KeyValue<number,string>, b: KeyValue<number,string>): number => {
    return a.key > b.key ? -1 : (b.key > a.key ? 1 : 0);
  }

  ngOnDestroy(): void
  {
    this.subscription.unsubscribe();
  }

}
