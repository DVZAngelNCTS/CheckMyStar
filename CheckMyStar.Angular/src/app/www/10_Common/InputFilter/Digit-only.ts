import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[digitsOnly]'
})
export class DigitsOnlyDirective {

  @HostListener('input', ['$event'])
  onInput(event: any) {
    const initial = event.target.value;

    // Supprime tout ce qui n'est pas un chiffre
    const filtered = initial.replace(/[^0-9]/g, '');

    if (initial !== filtered) {
      event.target.value = filtered;
      event.target.dispatchEvent(new Event('input')); // met à jour Angular
    }
  }

  @HostListener('keydown', ['$event'])
  onKeyDown(event: KeyboardEvent) {
    const allowedKeys = [
      'Backspace', 'Tab', 'ArrowLeft', 'ArrowRight', 'Delete'
    ];

    if (allowedKeys.includes(event.key)) return;

    // Bloque les touches non numériques
    if (!/^[0-9]$/.test(event.key)) {
      event.preventDefault();
    }
  }
}
