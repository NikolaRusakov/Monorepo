var builder = DistributedApplication.CreateBuilder(args);

var data = builder.AddAzurePostgresFlexibleServer("data").RunAsContainer();

// builder.AddProject<Projects.ContactService_ApiService>()
//     .WithReference(data);

var apiService = builder
	.AddProject<Projects.ContactService_ApiService>("contactservice")
	.WithReference(data);

// builder.AddProject<Projects.AspireApp1_ApiService>("apiservice");
//        .WithReference(postgresdb)
//        .WaitForCompletion(migration);

builder.Build().Run();
