using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Otterly.API;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.EnvironmentName == "AWS")
{
	builder.ConfigureAWS();
}

// Add services to the container.
builder.ConfigureServices()
	   //.ConfigureAutomapper()
	   .ConfigureDatabase()
	   .ConfigureMongoAccessServices();

builder.ConfigureAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (builder.Environment.EnvironmentName == "AWS")
{

}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
