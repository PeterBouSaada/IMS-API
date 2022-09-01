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
  headers: Array<string> = ["ID (temp)", "Part#", "Type", "Qty", "Location", "Height", "Width", "Length", "UOM", "Manufacturer", "Man. Phone #", "Used for", "Horsepower", "Amperage", "Voltage", "RPM"];
  API_string: string = "item";

  constructor() {}

  ngOnInit(): void {
  }

  deleteEvent(id: Event)
  {
    console.log("Delete Event on ID: " + id);
  }

  editEvent(id: Event)
  {
    console.log("Edit Event on ID: " + id);
  }

}
