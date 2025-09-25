import { HttpEvent, HttpHandlerFn, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs';
import { APP_CONFIG } from '../config/app-config';
import { inject } from '@angular/core';

export function urlInterceptor(
  req: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> {
  const { API_URL: appConfig } = inject(APP_CONFIG);

  return next(req.clone({ url: appConfig + req.url }));
}

// export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
//   const authToken = inject(AuthService).getAuthToken();
//   const newReq = req.clone({
//     headers: req.headers.append('X-Authentication-Token', authToken),
//   });
//   return next(newReq);
// }
