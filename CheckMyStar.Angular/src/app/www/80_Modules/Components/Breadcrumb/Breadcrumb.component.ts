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

        // 1. Construire les breadcrumbs
        this.breadcrumbs = this.buildBreadcrumbs(this.route.root);

        const url = this.router.url;
        const segments = url.split('/').filter(s => s);

        // 2. Pas de bouton retour sur le tableau de bord
        if (segments.length <= 1 || url === '/backhome' || url === '/fronthome') {
          this.parentUrl = null;
          return;
        }

        // 3. Parent logique via data.parent
        const deepest = this.getDeepestRoute(this.route);
        const parentLabel = deepest.snapshot.data['parent'];

        if (parentLabel) {
          const parentCrumb = this.breadcrumbs.find(b => b.label === parentLabel);
          if (parentCrumb) {
            this.parentUrl = parentCrumb.url;
            return;
          }
        }

        // 4. Parent générique : URL sans le dernier segment
        const parentUrl = '/' + segments.slice(0, -1).join('/');
        this.parentUrl = parentUrl;
      });
  }

  private getDeepestRoute(route: ActivatedRoute): ActivatedRoute {
    while (route.firstChild) {
      route = route.firstChild;
    }
    return route;
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

      const fullUrl = url;

      const label = child.snapshot.data['breadcrumb'];
      const icon = child.snapshot.data['icon'];
      const parent = child.snapshot.data['parent'];

      // 🔥 Parent(s) logique(s) pour l'affichage ET navigation correcte
      const parents: { label: string, icon: string, urlOffset: number }[] | undefined = child.snapshot.data['parents'];

      if (parents && parents.length > 0) {
        // Multi-level parents: urlOffset = nb of segments to strip from the end of fullUrl
        for (const p of parents) {
          const segments = fullUrl.split('/');
          const ancestorUrl = segments.slice(0, segments.length - p.urlOffset).join('/');
          if (!breadcrumbs.some(b => b.label === p.label)) {
            breadcrumbs.push({ label: p.label, icon: p.icon, url: ancestorUrl });
          }
        }
      } else if (parent) {
        const parentUrl = fullUrl.split('/').slice(0, -1).join('/');

        const parentBreadcrumb = {
          label: parent,
          icon: child.snapshot.data['icon'] ?? 'bi bi-house',
          url: parentUrl
        };

        if (!breadcrumbs.some(b => b.label === parentBreadcrumb.label)) {
          breadcrumbs.push(parentBreadcrumb);
        }
      }

      // Breadcrumb courant
      if (label) {
        breadcrumbs.push({ icon, label, url: fullUrl });
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
