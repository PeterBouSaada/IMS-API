import { KeyValue } from '@angular/common';
import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { Item } from 'src/app/Models/Item';

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

  constructor() {}

  ngOnInit(): void {
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

  stopCopy()
  {
    return false;
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

}
