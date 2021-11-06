import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/user';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  caption : string = "User List";
  testData: Array<User> = [
    {
      id : "asdasfasd",
      username: "pbousaada",
      password: "fake pass",
      salt: "test"
    },
    {
      id : "asda;khasd as",
      username: "jbousaada",
      password: "fake pass",
      salt: "test"
    }
  ]
  headers: Array<string> = ["ID", "username", "password", "salt"];

  constructor() { }

  ngOnInit(): void {
  }

}
