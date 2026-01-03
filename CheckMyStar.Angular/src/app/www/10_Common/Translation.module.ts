import { NgModule } from "@angular/core";
import { TranslateModule } from "@ngx-translate/core";
import { CommonModule } from "@angular/common";

@NgModule({
    imports: [CommonModule, TranslateModule],
    exports: [CommonModule, TranslateModule]
})
export class TranslationModule {}