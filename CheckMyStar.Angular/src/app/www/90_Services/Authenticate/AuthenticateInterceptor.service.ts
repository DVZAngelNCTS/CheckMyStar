import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, BehaviorSubject, throwError } from 'rxjs';
import { catchError, filter, switchMap, take } from 'rxjs/operators';
import { AuthenticateService } from './Authenticate.service';

@Injectable()
export class AuthenticateInterceptor implements HttpInterceptor {

  private isRefreshing = false;
  private refreshSubject = new BehaviorSubject<string | null>(null);

  constructor(private auth: AuthenticateService) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const token = this.auth.getAccessToken();

    const authReq = token
      ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
      : req;

    return next.handle(authReq).pipe(
      catchError(error => {

        // Token expiré → 401
        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.handle401(authReq, next);
        }

        return throwError(() => error);
      })
    );
  }

  private handle401(req: HttpRequest<any>, next: HttpHandler) {

    if (!this.isRefreshing) {

      this.isRefreshing = true;
      this.refreshSubject.next(null);

      return this.auth.refreshToken$().pipe(
        switchMap((tokens: any) => {

          this.isRefreshing = false;

          // Stocker les nouveaux tokens
          this.auth.setTokens(tokens.accessToken, tokens.refreshToken);

          this.refreshSubject.next(tokens.accessToken);

          // Rejouer la requête d’origine
          const newReq = req.clone({
            setHeaders: { Authorization: `Bearer ${tokens.accessToken}` }
          });

          return next.handle(newReq);
        }),
        catchError(err => {
          this.isRefreshing = false;
          this.auth.logout();
          return throwError(() => err);
        })
      );
    }

    // Si un refresh est déjà en cours → attendre
    return this.refreshSubject.pipe(
      filter(token => token !== null),
      take(1),
      switchMap(token => {
        const newReq = req.clone({
          setHeaders: { Authorization: `Bearer ${token}` }
        });
        return next.handle(newReq);
      })
    );
  }
}
