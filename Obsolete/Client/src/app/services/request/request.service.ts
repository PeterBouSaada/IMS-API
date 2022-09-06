import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Observable, observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { Item } from 'src/app/Models/Item';
import { User } from 'src/app/Models/user';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  BASE_URL: string = "http://localhost:5000/API/";

  constructor(private _http: HttpClient) {}

  private buildURL(endpoint: string) : string | null | undefined
  {
    if(endpoint.length < 1 || endpoint == null)
      return null;

    if(endpoint.charAt(0) == '/')
      endpoint = endpoint.substring(1);

    return this.BASE_URL + endpoint;
  }

  get(endpoint:string) : Observable<HttpResponse<any>> | null | undefined
  {
    if(!this.isTokenValid())
      return;
    
    let API_URL = this.buildURL(endpoint);
    if(API_URL == null)
      return;

    let headers = new HttpHeaders({
      Authorization: "Bearer " + localStorage.getItem("token")  
    });
    let options = {
      responseType: "json" as const,
      observe: "response" as const,
      headers: headers as HttpHeaders
    }
    
    return this._http.get<any>(API_URL, options);
  }

  post(endpoint:string, body: any, isLoggingIn: boolean = false) : Observable<HttpResponse<any>> | null | undefined
  {
    if(!isLoggingIn)
    {
      if(!this.isTokenValid())
        return;
    }

    let API_URL = this.buildURL(endpoint);
    if(API_URL == null)
      return;

    let headers = new HttpHeaders({
      Authorization: "Bearer " + localStorage.getItem("token")  
    });
    let options = {
      responseType: "json" as const,
      observe: "response" as const,
      headers: headers as HttpHeaders
    }
    
    return this._http.post<any>(API_URL, body, options);
  }

  delete(endpoint:string) : Observable<HttpResponse<any>> | null | undefined
  {
    if(!this.isTokenValid())
      return;
    
    let API_URL = this.buildURL(endpoint);
    if(API_URL == null)
      return;

    let headers = new HttpHeaders({
      Authorization: "Bearer " + localStorage.getItem("token")
    });
    let options = {
      responseType: "json" as const,
      observe: "response" as const,
      headers: headers as HttpHeaders
    }
    
    return this._http.delete<any>(API_URL, options);
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
