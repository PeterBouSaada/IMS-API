import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot,RouterStateSnapshot } from '@angular/router';
import { RequestService } from '../request/request.service';

@Injectable({
  providedIn: 'root'
})
export class UserNotLoggedInGuard {

  constructor(private _requestService: RequestService, private _router: Router) { }

  canActivate(route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean
  {
    if(this._requestService.isTokenValid())
    {
      this._router.navigate(['/']);
      return false;
    }
    return true;
  }

}
