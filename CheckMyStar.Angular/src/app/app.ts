import { Component, signal } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoaderComponent } from './www/70_Modules/Components/Loader/Loader.component';
import { LoaderManager } from './www/80_Services/Loader/Loader-manager.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  standalone: true,
  styleUrl: './app.css',
  imports: [RouterModule, LoaderComponent]
})
export class App {
  protected readonly title = signal('CheckMyStar.Angular');
  loaderVisible = false;

  constructor(manager: LoaderManager) { 
    manager.global$.subscribe(v => this.loaderVisible = v);
  }
}
