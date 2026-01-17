import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, finalize } from 'rxjs';
import { LoaderManager } from './Loader-manager.service';

@Injectable()
export class LoaderInterceptor implements HttpInterceptor {

  constructor(private manager: LoaderManager) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let timeoutId: any;

    // ⏳ Affiche le loader global seulement après 400ms
    timeoutId = setTimeout(() => {
      this.manager.showGlobal();
    }, 400);

    return next.handle(req).pipe(
      finalize(() => {
        clearTimeout(timeoutId);     // ❗ Empêche le loader global si la requête est rapide
        this.manager.hideGlobal();   // ❗ Cache le loader global s’il s’est affiché
      })
    );
  }
}
