import { KeyValue } from '@angular/common';
import { analyzeAndValidateNgModules } from '@angular/compiler';
import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Item } from 'src/app/Models/Item';
import { User } from 'src/app/Models/user';
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
  @Input() API_string: string;
  @Input() fields: any[];
  @Output() deleteEvent: EventEmitter<any> = new EventEmitter();
  @Output() editEvent: EventEmitter<any> = new EventEmitter();
  subscription : any;

  constructor(private _eventService: EventService, private _routerService: Router, private _requestService: RequestService) {
    this.fields = new Array(30);
  }

  ngOnInit(): void {
    this.subscription = this._eventService.subject.subscribe((data: string) =>
    {
      this.search(data);
    });
  }

  search(data: string)
  {
    let route = this._routerService.url;

    if(data.length < 1 || data == undefined)
    {
      this._requestService.get(this.API_string)?.subscribe(response => {
        this.data = response.body;
      });
    }
    else
    {

      let query = {username: data, part_number: data};
      this._requestService.post(this.API_string + "/search", query)?.subscribe(response => {
        this.data = response.body;
      });
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
    return this.data;
  }

  newObject()
  {
    let tempObject: any = new Object();
    if(this.API_string == "item")
    {
      tempObject = new Item();
    }
    else if(this.API_string == "user")
    {
      tempObject = new User();
    }

    let i: number = 0;
    for (let key in tempObject) {
      tempObject[key] = this.fields[i];
      i++;
    }

      this._requestService.post(this.API_string + "/add", tempObject)?.subscribe((response) => {
        if(response.status == 201)
        {
          console.log("created");
          this.search("");
        }
      });
    
      this.fields = [];
  }

  del(id : string)
  {
    this._requestService.delete(this.API_string + "/" + id)?.subscribe(response => {
      if(response.status == 200)
      {
        console.log("Deleted: \n" + response.body);
        this.search("");
      }
    });
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
