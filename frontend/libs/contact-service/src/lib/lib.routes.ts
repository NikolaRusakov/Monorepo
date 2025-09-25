import { Route } from '@angular/router';
import { ContactService } from './contact-service';

export const contactServiceRoutes: Route[] = [
  { path: '', component: ContactService },
];
