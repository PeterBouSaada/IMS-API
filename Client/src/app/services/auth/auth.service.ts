import { Injectable } from '@angular/core';
import { Observable,  } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { User } from 'src/app/Models/user';
import { RequestService } from '../request/request.service';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  
  constructor(private _requestService: RequestService) { }

  login(user: User) : Observable<HttpResponse<any>> | null | undefined
  {
    return this._requestService.post("user/Authenticate", user, true);
  }

}
