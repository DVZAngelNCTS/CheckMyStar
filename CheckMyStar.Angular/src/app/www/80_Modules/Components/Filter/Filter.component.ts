import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TranslationModule } from '../../../10_Common/Translation.module';
import { MiniLoaderComponent } from '../../Components/Loader/Mini/Loader-mini.component';
import { TooltipDirective  } from '../Tooltip/Tooltip.directive';

@Component({
    selector: 'app-filter',
    standalone: true,
    imports: [CommonModule, FormsModule, ReactiveFormsModule, TranslationModule, MiniLoaderComponent, TooltipDirective],
    templateUrl: './Filter.component.html',
    styleUrls: ['./Filter.component.css']
})
export class FilterComponent {
    @Input() loadingSearch = false; 
    @Input() loadingReset = false;

    @Output() searchClicked = new EventEmitter<void>(); 
    @Output() resetClicked = new EventEmitter<void>();

	constructor() { 
	}

    search(): void {
        this.searchClicked.emit();      
    }

    reset(): void {
        this.resetClicked.emit();
    }
}