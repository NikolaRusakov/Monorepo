Data-flow diagram


- generate remote in 3th party monorepo
```
nx generate @nx/react:federate-module libs/core/src/index.ts --name=core --remote=backoffice_core --verbose
```

- generate federated host (Angular)
```
npx nx generate @nx/angular:host --directory=apps/host --remotes=backoffice_core --addTailwind=true --bundler=rspack --dynamic=true --inlineStyle=true --inlineTemplate=true --style=scss --unitTestRunner=vitest --viewEncapsulation=ShadowDom --no-interactive
```