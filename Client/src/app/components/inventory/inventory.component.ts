import { Component, OnInit } from '@angular/core';
import { Item } from 'src/app/Models/Item';

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css']
})
export class InventoryComponent implements OnInit {

  testData: Array<Item>;
  constructor() {
    this.testData = new Array<Item>();
    this.testData.push({
      id : "test1",
      type : "test1",
      partNumber : "test1",
      qtyOnHand : 5,
      location : "test1",
      height : 5,
      width : 5,
      length : 5,
      UOM : "test1",
      manufacturer : "test1",
      manufacturerPhoneNumber : "test1",
      userFor : "test1",
      horsepower : 10,
      amperage : 10,
      volatge : 10,
      RPM : 100
    });
    this.testData.push({
      id : "test1",
      type : "test1",
      partNumber : "test1",
      qtyOnHand : 5,
      location : "test1",
      height : 5,
      width : 5,
      length : 5,
      UOM : "test1",
      manufacturer : "test1",
      manufacturerPhoneNumber : "test1",
      userFor : "test1",
      horsepower : 10,
      amperage : 10,
      volatge : 10,
      RPM : 100
    });
  }

  ngOnInit(): void {
  }

  forLimit(num: number) {
    if(num > this.testData.length)
    {
      let items: Array<Item> = [];
      let i: number;
      for(i = 0; i < num; i++)
      {
        if (i < this.testData.length)
        {
         items[i] = this.testData[i];        
        }
        else 
        {
          items[i] = new Item;
        }
      }
      console.log(items);
      return items;
    }
    console.log(this.testData);
    return this.testData;
  }

}
