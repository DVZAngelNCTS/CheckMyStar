import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomePageComponent } from './www/70_Modules/FrontOffice/Home/Home-page.component';
import { LoginPageComponent } from './www/70_Modules/FrontOffice/Login/Login-page.component';
import { AuthenticateGuardian } from './www/80_Services/AuthenticateGuardian.service';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'home', component: HomePageComponent, canActivate: [AuthenticateGuardian] },  // Protect home
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }