import {Injectable, Injector} from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/first';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';
import "rxjs/add/observable/throw";
import {AuthService} from '../services';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private injector: Injector) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let clone = req.clone();

    if (clone.url.toLowerCase().includes('/api/auth/validate')) {
      return next.handle(req);
    }

    return this.request(clone)
      .switchMap((req: HttpRequest<any>) => next.handle(req))
      .catch((error: HttpErrorResponse) => this.responseError(clone, error));
  }

  private request(req: HttpRequest<any>): Observable<HttpRequest<any> | HttpEvent<any>> {
    return this.addToken(req);
  }

  private addToken(req: HttpRequest<any>): Observable<HttpRequest<any>> {
    let authService = this.injector.get(AuthService);
    if (authService.token)
      req = req.clone({headers: req.headers.set('Authorization', authService.token)});
    return Observable.of(req);
  }

  private responseError(req: HttpRequest<any>, res: HttpErrorResponse): Observable<HttpEvent<any>> {
    if (res.status != 401) return Observable.throw(res);

    let authService = this.injector.get(AuthService);
    authService.logout();

    Observable.throw('session expired')
  }
}
