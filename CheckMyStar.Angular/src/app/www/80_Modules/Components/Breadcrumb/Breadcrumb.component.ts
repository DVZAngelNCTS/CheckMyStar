import { Component } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs/operators';
import { TranslationModule } from '../../../10_Common/Translation.module';

@Component({
  selector: 'app-breadcrumb',
  standalone: true,
  imports: [CommonModule, RouterModule, TranslationModule],
  templateUrl: './Breadcrumb.component.html',
  styleUrl: './Breadcrumb.component.css'
})
export class BreadcrumbComponent {
  breadcrumbs: Array<{ icon: string, label: string, url: string }> = [];

  parentUrl: string | null = null;
  
  constructor(private router: Router, private route: ActivatedRoute) {
    this.router.events
      .pipe(filter(event => event instanceof NavigationEnd))
      .subscribe(() => {
        this.breadcrumbs = this.buildBreadcrumbs(this.route.root);

      if (this.breadcrumbs.length > 1) { 
        const parent = this.breadcrumbs[this.breadcrumbs.length - 2];
      
        if (parent.url !== this.router.url) { 
          this.parentUrl = parent.url; 
        } else { 
          this.parentUrl = null; 
        } 
      } else { 
        this.parentUrl = null; 
      }
    });
  }

  private buildBreadcrumbs(route: ActivatedRoute, url: string = '', breadcrumbs: any[] = []): any[] {
    const children = route.children;

    if (children.length === 0) {
      return breadcrumbs;
    }

    for (const child of children) {
      const routeURL = child.snapshot.url.map(segment => segment.path).join('/');
      if (routeURL !== '') {
        url += `/${routeURL}`;
      }

      const label = child.snapshot.data['breadcrumb'];
      const icon = child.snapshot.data['icon'];
      const parent = child.snapshot.data['parent'];

      // Si la route a un parent logique → on l'ajoute AVANT
      if (parent) {
        const parentBreadcrumb = {
          label: parent,
          icon: 'bi bi-speedometer2', // ou récupéré dynamiquement si tu veux
          url: '/backhome' // URL du dashboard
        };

        // On évite les doublons
        if (!breadcrumbs.some(b => b.label === parentBreadcrumb.label)) {
          breadcrumbs.push(parentBreadcrumb);
        }
      }

      // Ajout du breadcrumb courant
      if (label) {
        breadcrumbs.push({ icon, label, url });
      }

      return this.buildBreadcrumbs(child, url, breadcrumbs);
    }

    return breadcrumbs;
  }

  goBack() {
    if (this.parentUrl) {
      this.router.navigate([this.parentUrl]);
    }
  }
}
