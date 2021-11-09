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
  API_string: string = "user";

  constructor() { }

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
