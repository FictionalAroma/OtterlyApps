using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Otterly.Site.StartupExtensions;

public static class AuthenticationStartup
{
	public static WebApplicationBuilder AddAuthentication(this WebApplicationBuilder builder)
	{
		var services = builder.Services;
		services.AddAuthentication(options =>
				{
					options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
					options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
				})
				.AddCookie(o =>
				{
					o.Cookie.SecurePolicy = CookieSecurePolicy.Always;
					o.Cookie.SameSite = SameSiteMode.Strict;
					o.Cookie.HttpOnly = true;
					o.LoginPath = "/bff/auth/login";
					o.LogoutPath = "/bff/auth/logout";
					o.Events.OnRedirectToLogin = context =>
					{
						context.RedirectUri = "/bff/auth/login";
						return Task.CompletedTask;
					};
				})
				.AddOpenIdConnect("Auth0", options => { ConfigureOpenIdConnect(options, builder.Configuration); });


		return builder;
	}

	private static void ConfigureOpenIdConnect(OpenIdConnectOptions options, IConfiguration config)
	{
		// Set the authority to your Auth0 domain
		options.Authority = $"https://{config["Auth0:Domain"]}";

		// Configure the Auth0 Client ID and Client Secret
		options.ClientId = config["Auth0:ClientId"];
		options.ClientSecret = config["Auth0:ClientSecret"];

		// Set response type to code
		options.ResponseType = OpenIdConnectResponseType.CodeIdToken;

		options.ResponseMode = OpenIdConnectResponseMode.FormPost;

		// Configure the scope
		options.Scope.Clear();
		options.Scope.Add("openid");
		options.Scope.Add("offline_access");

		options.GetClaimsFromUserInfoEndpoint = true;
		options.ClaimActions.Add(new MapAllClaimsAction());

		// Set the callback path, so Auth0 will call back to http://localhost:3000/callback
		// Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
		options.CallbackPath = new PathString("/bff/auth/logincallback");

		// Configure the Claims Issuer to be Auth0
		options.ClaimsIssuer = "Auth0";


		options.SaveTokens = true;


		options.Events = new OpenIdConnectEvents
						 {
							 // handle the logout redirection
							 OnRedirectToIdentityProviderForSignOut = (context) =>
							 {
								 var logoutUri =
									 $"https://{config["Auth0:Domain"]}/v2/logout?client_id={config["Auth0:ClientId"]}";

								 var postLogoutUri = context.Properties.RedirectUri;
								 if (!string.IsNullOrEmpty(postLogoutUri))
								 {
									 if (postLogoutUri.StartsWith("/"))
									 {
										 // transform to absolute
										 var request = context.Request;
										 postLogoutUri =
											 request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
									 }

									 logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
								 }

								 context.Response.Redirect(logoutUri);
								 context.HandleResponse();

								 return Task.CompletedTask;
							 },
							 OnRedirectToIdentityProvider = context =>
							 {
								 context.ProtocolMessage.SetParameter("audience", config["Auth0:ApiAudience"]);
								 context.Response.Redirect("/bff/auth/login");

								 return Task.CompletedTask;
							 }
						 };
	}
}
	