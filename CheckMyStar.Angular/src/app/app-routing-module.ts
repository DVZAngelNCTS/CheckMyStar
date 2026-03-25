import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BackHomePageComponent } from './www/80_Modules/BackOffice/Home/Home-page.component';
import { FrontHomePageComponent } from './www/80_Modules/FrontOffice/Home/Home-page.component';
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
import { DossiersPageComponent } from './www/80_Modules/BackOffice/Dossiers/Dossiers-page.component';
import { QuotesPageComponent } from './www/80_Modules/BackOffice/Quotes/Quotes-page.component';
import { InvoicesPageComponent } from './www/80_Modules/BackOffice/Invoices/Invoices-page.component';
import { FrontQuotesPageComponent } from './www/80_Modules/FrontOffice/Quotes/Quotes-page.component';
import { FrontQuoteCreatePageComponent } from './www/80_Modules/FrontOffice/Quotes/Create/Quote-create-page.component';
import { FrontQuoteDetailPageComponent } from './www/80_Modules/FrontOffice/Quotes/Detail/Quote-detail-page.component';
import { FrontQuoteEditPageComponent } from './www/80_Modules/FrontOffice/Quotes/Edit/Quote-edit-page.component';
import { FrontInvoicesPageComponent } from './www/80_Modules/FrontOffice/Invoices/Invoices-page.component';
import { FrontDossiersPageComponent } from './www/80_Modules/FrontOffice/Dossiers/Dossiers-page.component';
import { DossierDetailPageComponent } from './www/80_Modules/FrontOffice/Dossiers/Detail/Dossier-detail-page.component';
import { EvaluationPageComponent } from './www/80_Modules/FrontOffice/Dossiers/Evaluation/Evaluation-page.component';
import { EvaluationViewPageComponent } from './www/80_Modules/FrontOffice/Dossiers/EvaluationView/EvaluationView-page.component';
import { FrontDashboardComponent } from './www/80_Modules/FrontOffice/Home/Dashboard/Dashboard-page.component';
import { SocietyPageComponent } from './www/80_Modules/BackOffice/Societies/Society-page.component';
import { AddressPageComponent } from './www/80_Modules/BackOffice/Addresses/Address-page.component';

const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginPageComponent },
  { path: 'password', component: PasswordPageComponent },
  { path: 'forgot-password', component: ForgotPageComponent },
  {
    path: 'fronthome',
    component: FrontHomePageComponent,
    canActivate: [AuthenticateGuardian],
    data: { breadcrumb: 'FrontOfficeMenuSection.Home', icon: 'bi bi-house' },
    children: [
      { path: '', component: FrontDashboardComponent, data: { breadcrumb: 'FrontOfficeMenuSection.Dashboard', icon: 'bi bi-speedometer2' }},
      { path: 'devis', component: FrontQuotesPageComponent, data: { breadcrumb: 'FrontOfficeMenuSection.Devis', icon: 'bi bi-file-earmark-text' }},
      { path: 'devis/:id', component: FrontQuoteCreatePageComponent, data: { breadcrumb: 'FrontQuoteSection.FormTitle', icon: 'bi bi-file-earmark-text', parent: 'FrontOfficeMenuSection.Devis' }},
      { path: 'devis/:id/edit', component: FrontQuoteEditPageComponent, data: { breadcrumb: 'FrontQuoteSection.EditFormTitle', icon: 'bi bi-pencil', parents: [{ label: 'FrontOfficeMenuSection.Devis', icon: 'bi bi-file-earmark-text', urlOffset: 2 }] }},
      { path: 'devis/:id/view', component: FrontQuoteDetailPageComponent, data: { breadcrumb: 'FrontQuoteSection.PreviewTitle', icon: 'bi bi-eye', parents: [{ label: 'FrontOfficeMenuSection.Devis', icon: 'bi bi-file-earmark-text', urlOffset: 2 }] }},
      { path: 'dossiers', component: FrontDossiersPageComponent, data: { breadcrumb: 'FrontOfficeMenuSection.Dossiers', icon: 'bi bi-clipboard-check' }},
      { path: 'dossiers/:id', component: DossierDetailPageComponent, data: { breadcrumb: 'FrontDossiersSection.DossierDetail', icon: 'bi bi-folder2-open', parent: 'FrontOfficeMenuSection.Dossiers' }},
      { path: 'dossiers/:id/evaluation', component: EvaluationPageComponent, data: { breadcrumb: 'EvaluationSection.Title', icon: 'bi bi-star', parents: [{ label: 'FrontOfficeMenuSection.Dossiers', icon: 'bi bi-clipboard-check', urlOffset: 2 }, { label: 'FrontDossiersSection.DossierDetail', icon: 'bi bi-folder2-open', urlOffset: 1 }] }},
      { path: 'dossiers/:id/evaluation-view', component: EvaluationViewPageComponent, data: { breadcrumb: 'EvaluationViewSection.Title', icon: 'bi bi-clipboard-data', parents: [{ label: 'FrontOfficeMenuSection.Dossiers', icon: 'bi bi-clipboard-check', urlOffset: 2 }, { label: 'FrontDossiersSection.DossierDetail', icon: 'bi bi-folder2-open', urlOffset: 1 }] }},
      { path: 'factures', component: FrontInvoicesPageComponent, data: { breadcrumb: 'FrontOfficeMenuSection.Factures', icon: 'bi bi-receipt' }},
    ]
  },
  {
    path: 'backhome',
    component: BackHomePageComponent,
    canActivate: [AuthenticateGuardian],
    data: { breadcrumb: 'BackOfficeMenuSection.Home', icon: 'bi bi-house' },
    children: [
      { path: '', component: DashboardComponent, data: { breadcrumb: 'BackOfficeMenuSection.Dashboard', icon: 'bi bi-speedometer2' }},
      { path: 'activities', component: ActivityComponent, data: { breadcrumb: 'BackOfficeMenuSection.Activities', icon: 'bi-clock-history', parent: 'BackOfficeMenuSection.Dashboard' }},
      { path: 'addresses', component: AddressPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Addresses', icon: 'bi-envelope' }},
      { path: 'criteres', component: CriteresPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Criteres', icon: 'bi bi-star' }},
      { path: 'criteres/management', component: CriteresManagementPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Management', icon: 'bi bi-gear', parent: 'BackOfficeMenuSection.Criteres' }},
      { path: 'dossiers', component: DossiersPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Dossiers', icon: 'bi bi-folder2-open' }},
      { path: 'devis', component: QuotesPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Devis', icon: 'bi bi-file-earmark-text' }},
      { path: 'factures', component: InvoicesPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Factures', icon: 'bi bi-receipt' }},
      { path: 'roles', component: RolePageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Roles', icon: 'bi bi-shield-check' }},
      { path: 'societies', component: SocietyPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Societies', icon: 'bi bi-building' }},
      { path: 'users', component: UserPageComponent, data: { breadcrumb: 'BackOfficeMenuSection.Users', icon: 'bi bi-people' }},
    ]
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }