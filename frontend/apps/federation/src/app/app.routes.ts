import { NxWelcome } from './nx-welcome';
import { Route } from '@angular/router';
import { loadRemote } from '@module-federation/enhanced/runtime';
import { loadRemoteModule } from '@angular-architects/module-federation';
import {
  WebComponentWrapper,
  WebComponentWrapperOptions,
} from '@angular-architects/module-federation-tools';

export const appRoutes: Route[] = [
  {
    path: 'backoffice_core',
    loadChildren: () =>
      loadRemote<typeof import('backoffice_core/Routes')>(
        'backoffice_core/Routes'
      ).then((m) => m!.remoteRoutes),
  },

  {
    path: 'backoffice_core',
    loadChildren: () =>
      loadRemote<typeof import('backoffice_core/Routes')>(
        'backoffice_core/Routes'
      ).then((m) => {
        console.log('module', m);
        return m!.remoteRoutes;
      }),
  },
  {
    path: 'react',
    component: WebComponentWrapper,
    data: {
      // https://github.com/angular-architects/module-federation-plugin/tree/main/libs/mf-tools#important-angular-13
      type: 'module',
      remoteEntry: 'http://localhost:4200/remoteEntry.js',
      // remoteName: 'backoffice_core',
      exposedModule: './Routes',
      elementName: 'http-mfe-react-element',
    } as WebComponentWrapperOptions,
  },
  {
    path: 'contacts',
    loadComponent: () =>
      import('./infrastructure/contacts/contact-overview.component').then(
        (c) => c.default
      ),
  },
  // {
  //   path: '',
  //   component: NxWelcome,
  // },
];
