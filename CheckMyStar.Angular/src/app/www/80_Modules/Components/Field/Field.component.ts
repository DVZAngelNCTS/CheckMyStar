import { ControlContainer, FormControl, FormGroupDirective } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Component, computed, input, AfterContentInit, ContentChild,  ElementRef} from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'app-field',
  standalone: true,
  imports: [TranslateModule, CommonModule],
  templateUrl: './Field.component.html',
  viewProviders: [{ provide: ControlContainer, useExisting: FormGroupDirective }],
  styleUrls: ['./Field.component.css'],
})
export class FieldComponent implements AfterContentInit {
  @ContentChild('fieldControl', { read: ElementRef }) controlElement!: ElementRef;

  readonly = input<boolean>(false);
  disabled = input<boolean>(false);
  containerClass = input<string>('');
  label = input.required<string>();
  icon = input<string>('');
  labelClass = input<string>('w-7rem');
  labelTextPosition = input<'start' | 'end' | 'center'>('end');
  controlClass = input<string>('w-14rem');

  constructor(private controlContainer: ControlContainer) {}

  controlName = input.required<string>();

  get control(): FormControl {
    return this.controlContainer.control?.get(this.controlName()) as FormControl;
  }

  get invalid(): boolean {
    return this.control?.invalid && this.control?.touched;
  }

  labelClassStr = computed(() => `me-2 ${this.labelClass()} text-${this.labelTextPosition()}`);

  ngAfterContentInit() { 
    if (this.controlElement) { 
      if (this.readonly()) { 
        this.controlElement.nativeElement.setAttribute('readonly', 'true'); 
      } 
      
      if (this.disabled()) { 
        this.controlElement.nativeElement.setAttribute('disabled', 'true'); 
      } 
    } 
  }  
}
