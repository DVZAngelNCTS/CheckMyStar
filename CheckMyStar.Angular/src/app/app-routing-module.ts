import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FrontHomePageComponent } from './www/80_Modules/FrontOffice/Home/Home-page.component';
import { BackHomePageComponent } from './www/80_Modules/BackOffice/Home/Home-page.component';
import { LoginPageComponent } from './www/80_Modules/FrontOffice/Login/Login-page.component';
import { AuthenticateGuardian } from './www/90_Services/Authenticate/AuthenticateGuardian.service';
import { RolePageComponent } from './www/80_Modules/BackOffice/Roles/Role-page.component';
import { UserPageComponent } from './www/80_Modules/BackOffice/Users/User-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'fronthome', component: FrontHomePageComponent, canActivate: [AuthenticateGuardian] },
  { path: 'backhome', component: BackHomePageComponent, canActivate: [AuthenticateGuardian], data: { breadcrumb: 'BackOfficeMenuSection.Home', icon: 'bi bi-house' },
    children: [
      { path : 'roles', component:  RolePageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Roles', icon: 'bi bi-shield-check' }},
      { path : 'users', component:  UserPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Users', icon: 'bi bi-people' }}
    ]
   },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }