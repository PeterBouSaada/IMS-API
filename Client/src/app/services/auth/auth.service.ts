import { Injectable } from '@angular/core';
import { Observable,  } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { User } from 'src/app/Models/user';

@Injectable({
  providedIn: 'root'
})

export class AuthService {

  login_url = "http://localhost:5000/API/user/authenticate";
  
  constructor(private _http:HttpClient) { }

  login(user: User) : Observable<HttpResponse<any>>{
    let headers = {
      observe: 'response'
    }
    return this._http.post<any>(this.login_url, user,{ headers, observe: 'response' });
  }

  isTokenValid()
  {
    let token = localStorage.getItem("token")?.toString();
    if(token == null)
    {
      return false;
    }
    return true;
  }

  private tokenExpired(token: string) {
    const expiry = (JSON.parse(atob(token.split('.')[1]))).exp;
    return (Math.floor((new Date).getTime() / 1000)) >= expiry;
  }
}
