import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { InventoryComponent } from './components/inventory/inventory.component';
import { LoginComponent } from './components/login/login.component';
import { AuthService } from './services/auth.service';

const routes: Routes = [
  {path: 'Login', component: LoginComponent},
  {path: 'Inventory', component: InventoryComponent},
  {path: '', component: AppComponent, canActivate: [AuthService]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
