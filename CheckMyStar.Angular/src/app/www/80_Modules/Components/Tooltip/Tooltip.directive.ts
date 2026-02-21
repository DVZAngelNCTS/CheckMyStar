import { Directive, ElementRef, HostListener, Input, Renderer2 } from '@angular/core';

@Directive({
  selector: '[appTooltip]',
  standalone: true
})
export class TooltipDirective {
  @Input('appTooltip') text = '';

  private tooltip?: HTMLElement;

  constructor(private host: ElementRef, private renderer: Renderer2) {}

  ngOnDestroy() {
    this.hide();
  }

  @HostListener('window:beforeunload')
  onBeforeUnload() {
    this.hide();
  }

  @HostListener('mouseenter')
  show() {
    if (!this.text) return;

    this.hide();

    const hostEl = this.host.nativeElement;

    const tooltip = this.renderer.createElement('div');
    this.renderer.addClass(tooltip, 'app-tooltip');

    const icon = hostEl.querySelector('i')?.cloneNode(true);
    const content = this.renderer.createElement('div');
    this.renderer.setStyle(content, 'display', 'flex');
    this.renderer.setStyle(content, 'align-items', 'center');
    this.renderer.setStyle(content, 'gap', '6px');

    if (icon) this.renderer.appendChild(content, icon);
    this.renderer.appendChild(content, this.renderer.createText(this.text));
    this.renderer.appendChild(tooltip, content);

    this.renderer.appendChild(document.body, tooltip);
    this.tooltip = tooltip;

    // Position temporaire pour mesurer
    this.renderer.setStyle(tooltip, 'top', `-9999px`);
    this.renderer.setStyle(tooltip, 'left', `-9999px`);

    requestAnimationFrame(() => {
      const hostRect = hostEl.getBoundingClientRect();
      const tooltipRect = tooltip.getBoundingClientRect();
      const viewportWidth = window.innerWidth;
      const viewportHeight = window.innerHeight;

      // 1) Choix vertical (haut / bas) – comme avant
      const spaceTop = hostRect.top;
      const spaceBottom = viewportHeight - hostRect.bottom;

      let position: 'top' | 'bottom' | 'left' | 'right' = 'top';

      if (spaceTop >= tooltipRect.height + 12) {
        position = 'top';
      } else if (spaceBottom >= tooltipRect.height + 12) {
        position = 'bottom';
      }

      // 2) Calcul provisoire de la position horizontale si top/bottom
      let top = 0;
      let left = 0;

      if (position === 'top') {
        top = hostRect.top - tooltipRect.height - 8;
        left = hostRect.left + hostRect.width / 2 - tooltipRect.width / 2;
      } else if (position === 'bottom') {
        top = hostRect.bottom + 8;
        left = hostRect.left + hostRect.width / 2 - tooltipRect.width / 2;
      }

      // 3) Vérifier si ça déborde à droite ou à gauche → forcer gauche/droite
      const wouldOverflowLeft = left < 0;
      const wouldOverflowRight = left + tooltipRect.width > viewportWidth;

      if (wouldOverflowRight) {
        position = 'left';
      } else if (wouldOverflowLeft) {
        position = 'right';
      }

      // 4) Position finale selon la direction
      if (position === 'top') {
        top = hostRect.top - tooltipRect.height - 8;
        left = hostRect.left + hostRect.width / 2 - tooltipRect.width / 2;
      }

      if (position === 'bottom') {
        top = hostRect.bottom + 8;
        left = hostRect.left + hostRect.width / 2 - tooltipRect.width / 2;
      }

      if (position === 'left') {
        top = hostRect.top + hostRect.height / 2 - tooltipRect.height / 2;
        left = hostRect.left - tooltipRect.width - 8;
      }

      if (position === 'right') {
        top = hostRect.top + hostRect.height / 2 - tooltipRect.height / 2;
        left = hostRect.right + 8;
      }

      this.renderer.setStyle(tooltip, 'top', `${top}px`);
      this.renderer.setStyle(tooltip, 'left', `${left}px`);

      tooltip.classList.remove('top', 'bottom', 'left', 'right');
      this.renderer.addClass(tooltip, position);
      this.renderer.addClass(tooltip, 'show');
    });
  }

  @HostListener('mouseleave')
  hide() {
    if (this.tooltip) {
      this.renderer.removeChild(document.body, this.tooltip);
      this.tooltip = undefined;
    }
  }
}
