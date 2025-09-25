import { InjectionToken } from '@angular/core';
import { environment } from 'apps/federation/src/environments/environment';

export const APP_CONFIG = new InjectionToken<typeof environment>(
  'Application config'
);
