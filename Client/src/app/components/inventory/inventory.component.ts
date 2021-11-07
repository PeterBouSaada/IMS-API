import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/Models/Item';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {
  caption : string = "Inventory List";
  data: Array<Item> = [];
  headers: Array<string> = ["ID", "Part#", "Type", "Qty", "Location", "Height", "Width", "Length", "UOM", "Manufacturer", "Man. Phone #", "Used for", "Horsepower", "Amperage", "Voltage", "RPM"];

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

  viewEvent(id: Event)
  {
    console.log("View Event on ID: " + id);
  }

  editEvent(id: Event)
  {
    console.log("Edit Event on ID: " + id);
  }

}
