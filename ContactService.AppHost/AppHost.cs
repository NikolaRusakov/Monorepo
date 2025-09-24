var builder = DistributedApplication.CreateBuilder(args);
var apiService = builder.AddProject<Projects.ContactService_ApiService>("contactservice");

// builder.AddProject<Projects.AspireApp1_ApiService>("apiservice");
//        .WithReference(postgresdb)
//        .WaitForCompletion(migration);
       
builder.Build().Run();
