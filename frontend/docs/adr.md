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



## BE

- add DinD + Networking https://learn.microsoft.com/en-us/dotnet/aspire/get-started/dev-containers#advanced-container-networking
- Local Dev containers https://learn.microsoft.com/en-us/dotnet/aspire/azure/integrations-overview#local-containers
- explicit Docker container start https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/orchestrate-resources?tabs=docker#configure-explicit-resource-start

- Add as Auth
    ```CSharp
    var builder = WebApplication.CreateBuilder(args);
    
    //  https://www.rodyvansambeek.com/blog/using-supabase-auth-with-dotnet
    var supasbaseSignatureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(supabaseSecretKey));
    var validIssuers = "https://<your supabase project id>.supabase.co/auth/v1";
    var validAudiences = new List<string>() { "authenticated" };
    
    builder.Services.AddAuthentication().AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = supabaseSignatureKey,
            ValidAudiences = validAudiences,
            ValidIssuer = validIssuer
        };
    });
    ```