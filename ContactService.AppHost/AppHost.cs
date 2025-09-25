var builder = DistributedApplication.CreateBuilder(args);

var data = builder.AddAzurePostgresFlexibleServer("data").RunAsContainer();
var infra = "infrastructure";
var minio = builder.AddContainer("minio", "minio/minio:latest")
    .WithEnvironment("MINIO_ROOT_USER", "minioadmin")
    .WithEnvironment("MINIO_ROOT_PASSWORD", "minioadmin")
    .WithArgs("server", "/data", "--console-address", ":9001")
    .WithHttpEndpoint(9000, 9000, name: "api")
    .WithHttpEndpoint(9001, 9001, name: "console");


var verdaccio = builder.AddDockerfile("verdaccio", $"{infra}/verdaccio", "Dockerfile")
    .WithEnvironment("VERDACCIO_PORT", "4873")
    .WithHttpEndpoint(port: 4873, targetPort: 4873, name: "verdaccio")
    .WithVolume($"{infra}/verdaccio/conf", "/verdaccio/conf'");

var apiService = builder
    .AddProject<Projects.ContactService_ApiService>("contactservice")
    .WithHttpHealthCheck("/health")
    .WithReference(data)
    .WaitFor(data);

builder.Build().Run();
