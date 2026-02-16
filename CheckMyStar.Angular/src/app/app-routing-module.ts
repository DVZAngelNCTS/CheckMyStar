import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BackHomePageComponent } from './www/80_Modules/BackOffice/Home/Home-page.component';
import { LoginPageComponent } from './www/80_Modules/FrontOffice/Login/Login-page.component';
import { AuthenticateGuardian } from './www/90_Services/Authenticate/AuthenticateGuardian.service';
import { RolePageComponent } from './www/80_Modules/BackOffice/Roles/Role-page.component';
import { UserPageComponent } from './www/80_Modules/BackOffice/Users/User-page.component';
import { DashboardComponent } from './www/80_Modules/BackOffice/Home/Dashboard/Dashboard-page.component';
import { ActivityComponent } from './www/80_Modules/BackOffice/Home/Dashboard/Activity/Activity-page.component';
import { CriteresPageComponent } from './www/80_Modules/BackOffice/Criteres/Criteres-page.component';
import { CriteresManagementPageComponent } from './www/80_Modules/BackOffice/Criteres/CriteresManagement/Criteres-management-page.component';
import { PasswordPageComponent } from './www/80_Modules/FrontOffice/Login/Password/Password-page.component';
import { ForgotPageComponent } from './www/80_Modules/FrontOffice/Login/Forgot/Forgot-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'password', component: PasswordPageComponent },
  { path: 'forgot-password', component: ForgotPageComponent },
  {
    path: 'backhome',
    component: BackHomePageComponent,
    canActivate: [AuthenticateGuardian],
    data: { breadcrumb: 'BackOfficeMenuSection.Home', icon: 'bi bi-house' },
    children: [
      { path: '', component: DashboardComponent, data: { breadcrumb: 'BackOfficeMenuSection.Dashboard', icon: 'bi bi-speedometer2' }},
      { path: 'activities', component: ActivityComponent, data: { breadcrumb: 'BackOfficeMenuSection.Activities', icon: 'bi-clock-history', parent: 'BackOfficeMenuSection.Dashboard' }},
      { path: 'roles', component: RolePageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Roles', icon: 'bi bi-shield-check' }},
      { path: 'users', component: UserPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Users', icon: 'bi bi-people' }},
      { path: 'criteres', component: CriteresPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Criteres', icon: 'bi bi-star' }},
      { path: 'criteres/management', component: CriteresManagementPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Management', icon: 'bi bi-gear', parent: 'BackOfficeMenuSection.Criteres' }}          
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }