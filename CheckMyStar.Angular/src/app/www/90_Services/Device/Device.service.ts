import { Injectable } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Injectable({ providedIn: 'root' })
export class DeviceService {

  isMobile = false;
  isTablet = false;
  isDesktop = false;

  constructor(private breakpointObserver: BreakpointObserver) {

    // Détection Angular CDK (optionnelle)
    this.breakpointObserver.observe([
      Breakpoints.Handset,
      Breakpoints.Tablet,
      Breakpoints.Web
    ]).subscribe(result => {
      this.isMobile = result.breakpoints[Breakpoints.Handset] ?? false;
      this.isTablet = result.breakpoints[Breakpoints.Tablet] ?? false;
    });

    // Détection fiable du desktop
    this.isDesktop = matchMedia('(pointer: fine)').matches;

    // Mise à jour si l’utilisateur change de device (rare mais propre)
    matchMedia('(pointer: fine)').addEventListener('change', e => {
      this.isDesktop = e.matches;
    });
  }
}

