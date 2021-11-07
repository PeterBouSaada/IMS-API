import { Component, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './services/auth/auth.service';
import { RequestService } from './services/request/request.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  isLoggedIn: boolean;

  constructor(private _requestService: RequestService , private _router: Router) {
    this._router.navigate(['/Inventory']);
  }

  ngOnInit(): void {}

  checkLogin()
  {
    this.isLoggedIn = this._requestService.isTokenValid();
  }

}
