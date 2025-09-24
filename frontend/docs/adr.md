Data-flow diagram


- generate remote in 3th party monorepo
```
nx generate @nx/react:federate-module libs/core/src/index.ts --name=core --remote=backoffice_core --verbose
```

- generate federated host (Angular)
```
npx nx generate @nx/angular:host --directory=apps/host --remotes=backoffice_core --addTailwind=true --bundler=rspack --dynamic=true --inlineStyle=true --inlineTemplate=true --style=scss --unitTestRunner=vitest --viewEncapsulation=ShadowDom --no-interactive
```
- fix angular module federation deps 
https://github.com/Fictor86/nx-angular-host-react-mfe/blob/main/apps/host-angular/module-federation.config.js

- create a `http-mfe-react-element` web component at React bootsrap entry
https://github.com/Fictor86/nx-angular-host-react-mfe/blob/main/apps/http-mfe-react/src/bootstrap.tsx

- include type module for Angular 13+
[link](https://github.com/angular-architects/module-federation-plugin/tree/main/libs/mf-tools#important-angular-13)

