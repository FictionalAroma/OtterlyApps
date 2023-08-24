using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policyBuilder =>
	{
		var hosts = builder.Configuration.GetSection("CORSHosts").Get<string[]>();
		policyBuilder.WithOrigins(hosts).SetIsOriginAllowedToAllowWildcardSubdomains();
		policyBuilder.AllowAnyMethod();
		policyBuilder.AllowAnyHeader();
	});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseForwardedHeaders(new ForwardedHeadersOptions
							{
								ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
							});
	app.UseHsts();

}
else
{
	app.UseHttpsRedirection();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", ()=> "baseURL Test Direct Access Test");

app.Run();
