import { ModuleFederationConfig } from '@nx/module-federation';
const config: ModuleFederationConfig = {
  name: 'backoffice_core',
  exposes: {
    './Module': './src/remote-entry.ts',
    './core': '../../libs/core/src/index.ts',
    './Routes': './src/remote-entry.ts',
    // './Routes': 'backoffice_core/src/app/remote-entry/entry.routes.ts',
  },
};
/**
 * Nx requires a default export of the config to allow correct resolution of the module federation graph.
 **/
export default config;
