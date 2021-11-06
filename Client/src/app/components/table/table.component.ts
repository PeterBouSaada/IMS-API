import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { Item } from 'src/app/Models/Item';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.css']
})

export class TableComponent implements OnInit {

  @Input() caption: string;
  @Input() headers: Array<string>;
  @Input() data: Array<Item>;

  constructor() {}

  ngOnInit(): void {
  }

  forLimit(num: number) {
    if(num > this.data.length)
    {
      let items: Array<Item> = [];
      let i: number;
      for(i = 0; i < num; i++)
      {
        if (i < this.data.length)
        {
         items[i] = this.data[i];        
        }
        else 
        {
          items[i] = new Item;
        }
      }
      console.log(items);
      return items;
    }
    console.log(this.data);
    return this.data;
  }

}
