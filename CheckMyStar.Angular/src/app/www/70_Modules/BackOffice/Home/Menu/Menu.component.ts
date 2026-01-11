import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthenticateService } from '../../../../80_Services/Authenticate.service';
import { TranslationModule } from '../../../../10_Common/Translation.module';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [CommonModule, RouterModule, TranslationModule],
  templateUrl: './Menu.component.html'
})
export class MenuComponent implements OnInit {
  collapsed = true;
  @Output() openChange = new EventEmitter<boolean>();

  constructor(private authenticateService: AuthenticateService) {}

  ngOnInit(): void {
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