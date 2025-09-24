import { NxWelcome } from './nx-welcome';
import { Route } from '@angular/router';
import { loadRemote } from '@module-federation/enhanced/runtime';

export const appRoutes: Route[] = [
  // {
  //   path: 'remote',
  //   loadChildren: () =>
  //     loadRemote<typeof import('backoffice_core/Routes')>(
  //       'backoffice_core/Routes',
  //     ).then((m) => m!.remoteRoutes),
  // },
  // },
  // {
  //   path: 'remote',
  //   loadRemoteModule('backoffice_core', './Routes').then((m) => m.remoteRoutes),
  // },
  {
    path: 'backoffice_core',
    loadChildren: () =>
      loadRemote<typeof import('backoffice_core/Routes')>(
        'backoffice_core/Routes'
      ).then((m) => m!.remoteRoutes),
  },
  {
    path: '',
    component: NxWelcome,
  },
];
