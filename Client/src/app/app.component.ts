import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoginComponent } from './components/login/login.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  loggedIn: boolean = false;

  constructor(private _router: Router) {}

  ngOnInit(): void {
  }

}