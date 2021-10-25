import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;
  message: string;

  constructor(private _authService:AuthService) {}

  ngOnInit(): void {
  }

  login(): void {
    if(this.username == null || this.password == null)
    {
      this.message = "both username and password required";
      return;
    }
    this.message = "";
  }
 
}
