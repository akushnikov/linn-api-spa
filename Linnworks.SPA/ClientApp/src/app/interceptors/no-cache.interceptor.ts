import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class NoCacheInterceptor implements HttpInterceptor {

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		if (req.method === 'GET') {
			const newUrl = `${req.url}${req.url.indexOf('?') === -1 ? '?' : '&'}_t=${Date.now()}`;
			req = req.clone({ url: newUrl });
		}
		return next.handle(req);
	}

}
