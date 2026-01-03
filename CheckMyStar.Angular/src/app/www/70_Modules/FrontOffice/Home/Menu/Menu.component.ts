import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticateService } from '../../../../80_Services/Authenticate.service';

@Component({
  selector: 'app-menu',
  standalone: true,
  imports: [CommonModule],
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