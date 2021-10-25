import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { InventoryComponent } from './components/inventory/inventory.component';
import { LoginComponent } from './components/login/login.component';
import { AuthService } from './services/auth.service';
import { UserLoggedInGuard } from './services/guards/user-logged-in-guard.service';
import { UserNotLoggedInGuard } from './services/guards/user-not-logged-in-guard.service';

const routes: Routes = [
  {path: 'Login', component: LoginComponent, canActivate: [UserNotLoggedInGuard]},
  {path: 'Inventory', component: InventoryComponent, canActivate: [UserLoggedInGuard]},
  {path: '', component: AppComponent, canActivate: [UserLoggedInGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
