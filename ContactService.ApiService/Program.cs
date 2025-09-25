// using ContactService.ApiService;
// using ContactService.ApiService.Controllers;

using Carter;
using ContactsApi.Models;
using ContactsApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire service defaults
// builder.AddServiceDefaults();

// Add services to the container.
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();
app.MapCarter();

// app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
	var context = scope.ServiceProvider.GetRequiredService<ContactsApi.Data.ContactsDbContext>();
	context.Database.EnsureCreated();
}

app.Run();
