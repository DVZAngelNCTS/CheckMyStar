import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { FrontHomePageComponent } from './www/70_Modules/FrontOffice/Home/Home-page.component';
import { BackHomePageComponent } from './www/70_Modules/BackOffice/Home/Home-page.component';
import { MenuComponent } from './www/70_Modules/FrontOffice/Home/Menu/Menu.component';
import { TranslationModule } from './www/10_Common/Translation.module';
import { TRANSLATE_HTTP_LOADER_CONFIG } from '@ngx-translate/http-loader';
import { TranslationInitService } from './www/80_Services/Translation.service';
import { AuthenticateInterceptor } from './www/80_Services/AuthenticateInterceptor.service'
import { HTTP_INTERCEPTORS } from '@angular/common/http'

export function HttpLoaderFactory(http: HttpClient) { return new TranslateHttpLoader(); }

@NgModule({
  declarations: [
    App
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    TranslationModule,
    TranslateModule.forRoot({
      loader: { 
        provide: TranslateLoader,
        useClass: TranslateHttpLoader
    }}),
    FrontHomePageComponent,
    BackHomePageComponent,
    MenuComponent
  ],
  providers: [
    {
      provide: TRANSLATE_HTTP_LOADER_CONFIG, 
      useValue: { 
        prefix: './Assets/i18n/', 
        suffix: '.json' },
    },
    { 
      provide: HTTP_INTERCEPTORS, 
      useClass: AuthenticateInterceptor, 
      multi: true },
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule {
  constructor(private translationInit: TranslationInitService) {}
}