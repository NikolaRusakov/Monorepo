// using ContactService.ApiService;
// using ContactService.ApiService.Controllers;

using Carter;
using ContactsApi.Data;
using ContactsApi.Models;
using ContactsApi.Services;
using Microsoft.EntityFrameworkCore;
using Aspire.Npgsql;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();
builder.Services.AddScoped<IContactService, ContactServiceImpl>();

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
	});
});

// builder.Services.AddCarter(configurator: c =>
//     {
//         c.WithResponseNegotiator<CustomResponseNegotiator>();
//         c.WithModule<MyModule>();
//         c.WithValidator<TestModelValidator>();
//     });

var databaseName = "contactsdb";
builder.AddNpgsqlDataSource(connectionName: databaseName);

builder.Services.AddDbContext<ContactsDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString(databaseName)
		?? throw new InvalidOperationException("Connection string 'postgres' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();
app.MapCarter();


// app.MapDefaultEndpoints();


using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<ContactsDbContext>();
	context.Database.EnsureCreated();
	context.Database.Migrate();
}

app.Run();
