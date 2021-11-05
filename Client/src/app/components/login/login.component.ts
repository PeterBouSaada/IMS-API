import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/Models/user';
import { AuthService } from 'src/app/services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string;
  password: string;
  message: string;

  constructor(private _authService:AuthService, private _router: Router) {}

  ngOnInit(): void {
  }

  login(): void {
    if(this.username == null || this.password == null)
    {
      this.message = "both username and password required";
      return;
    }
    this.message = "";
    let user: User = new User();
    user.username = this.username;
    user.password = this.password;
    this._authService.login(user).subscribe((response) => {
      if(response.status == 200)
      {
        localStorage.setItem("token", response.body.token);
        this._router.navigate(['/Inventory']);
      }
    });
  }
 
}
