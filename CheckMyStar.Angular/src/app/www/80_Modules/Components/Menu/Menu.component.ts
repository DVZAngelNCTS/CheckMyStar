import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthenticateService } from '../../../90_Services/Authenticate/Authenticate.service';
import { MENU_CONFIG } from './Menu.config';
import { EnumRole } from '../../../10_Common/Enumerations/EnumRole';
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [CommonModule, RouterModule, TranslationModule],
  templateUrl: './Menu.component.html'
})
export class MenuComponent implements OnInit {

  collapsed = true;
  @Output() openChange = new EventEmitter<boolean>();

  menuItems: { label: string, route?: string, action?: string }[] = [];

  constructor(private authenticateService: AuthenticateService) {}

  ngOnInit(): void {
    const user = this.authenticateService.getCurrentUser();
    const role = user?.role as EnumRole;

    this.menuItems = MENU_CONFIG[role] ?? [];

    this.openChange.emit(!this.collapsed);
  }

  toggle() {
    this.collapsed = !this.collapsed;
    this.openChange.emit(!this.collapsed);
  }

  logout() {
    this.authenticateService.logout();
  }
}
