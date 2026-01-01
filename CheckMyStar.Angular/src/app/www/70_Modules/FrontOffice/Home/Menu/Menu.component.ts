
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
	selector: 'app-menu',
	standalone: true,
	imports: [CommonModule],
	templateUrl: './Menu.component.html'
})
export class MenuComponent implements OnInit {
	collapsed = true;
	@Output() openChange = new EventEmitter<boolean>();

	ngOnInit(): void {
		this.openChange.emit(!this.collapsed);
	}

	toggle() {
		this.collapsed = !this.collapsed;
		this.openChange.emit(!this.collapsed);
	}
}
