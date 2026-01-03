import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

@Injectable({ providedIn: 'root' }) 
export class TranslationInitService { 
    constructor(private translate: TranslateService) { 
        translate.setDefaultLang('fr'); 
        translate.use('fr'); } }
    