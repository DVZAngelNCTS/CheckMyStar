import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FrontHomePageComponent } from './www/70_Modules/FrontOffice/Home/Home-page.component';
import { BackHomePageComponent } from './www/70_Modules/BackOffice/Home/Home-page.component';
import { LoginPageComponent } from './www/70_Modules/FrontOffice/Login/Login-page.component';
import { AuthenticateGuardian } from './www/80_Services/AuthenticateGuardian.service';
import { RolePageComponent } from './www/70_Modules/BackOffice/Roles/Role-page.component';
import { UserPageComponent } from './www/70_Modules/BackOffice/Users/User-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'fronthome', component: FrontHomePageComponent, canActivate: [AuthenticateGuardian] },
  { path: 'backhome', component: BackHomePageComponent, canActivate: [AuthenticateGuardian],
    children: [
      { path : 'roles', component:  RolePageComponent},
      { path : 'users', component:  UserPageComponent}
    ]
   },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }