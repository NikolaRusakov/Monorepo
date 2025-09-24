var builder = DistributedApplication.CreateBuilder(args);
var apiService = builder.AddProject<Monorepo.ContactService>("contactservice");

// builder.AddProject<Projects.AspireApp_ApiService>("apiservice")
//        .WithReference(postgresdb)
//        .WaitForCompletion(migration);
       
builder.Build().Run();
