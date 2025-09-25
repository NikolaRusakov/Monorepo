var builder = DistributedApplication.CreateBuilder(args);



var postgres = builder.AddPostgres("postgres"/* , username, password */)
                      .WithPgAdmin(pgAdmin => pgAdmin.WithHostPort(5050)); ;
var databaseName = "contactsdb";

var creationScript = $$"""
    -- Create the database
    CREATE DATABASE {{databaseName}};

    """;

var db = postgres.AddDatabase(databaseName)
                 .WithCreationScript(creationScript);

var infra = "infrastructure";
var minio = builder.AddContainer("minio", "minio/minio:latest")
    .WithEnvironment("MINIO_ROOT_USER", "minioadmin")
    .WithEnvironment("MINIO_ROOT_PASSWORD", "minioadmin")
    .WithArgs("server", "/data", "--console-address", ":9001")
    .WithHttpEndpoint(9000, 9000, name: "api")
    .WithHttpEndpoint(9001, 9001, name: "console");


var verdaccioImage = builder.AddDockerfile("my-verdaccio", $"{infra}/verdaccio", "Dockerfile")
    .WithBuildArg("platform", "linux/arm64")
    .WithEnvironment("VERDACCIO_PORT", "4873")
    .WithHttpEndpoint(port: 4873, targetPort: 4873, name: "verdaccio")
    .WithLifetime(ContainerLifetime.Persistent);

var apiService = builder
    .AddProject<Projects.ContactService_ApiService>("contactservice")
    .WithHttpHealthCheck("/health")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
