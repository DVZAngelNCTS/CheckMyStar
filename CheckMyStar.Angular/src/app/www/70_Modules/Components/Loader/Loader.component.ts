import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-loader',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './Loader.component.html',
  styleUrls: ['./Loader.component.css']
})
export class LoaderComponent {
  @Input() visible = false;
}
