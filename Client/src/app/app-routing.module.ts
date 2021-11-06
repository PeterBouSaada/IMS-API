import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { InventoryComponent } from './components/inventory/inventory.component';
import { LoginComponent } from './components/login/login.component';
import { LogoutComponent } from './components/logout/logout.component';
import { UsersComponent } from './components/users/users.component';
import { UserLoggedInGuard } from './services/guards/user-logged-in-guard.service';
import { UserNotLoggedInGuard } from './services/guards/user-not-logged-in-guard.service';

const routes: Routes = [
  {path: 'Login', component: LoginComponent, canActivate: [UserNotLoggedInGuard]},
  {path: 'Logout', component: LogoutComponent, canActivate: [UserLoggedInGuard]},
  {path: 'Inventory', component: InventoryComponent, canActivate: [UserLoggedInGuard]},
  {path: 'Users', component: UsersComponent, canActivate: [UserLoggedInGuard]},
  {path: '', component: AppComponent, canActivate: [UserLoggedInGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
