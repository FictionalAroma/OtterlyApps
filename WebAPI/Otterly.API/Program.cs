using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Otterly.API;

var builder = WebApplication.CreateBuilder(args);
if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS")))
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
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
