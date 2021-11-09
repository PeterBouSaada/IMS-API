import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  caption : string = "User List";
  testData: Array<User> = []
  headers: Array<string> = ["ID", "Username", "Password", "Salt"];

  constructor() { }

  ngOnInit(): void {
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
