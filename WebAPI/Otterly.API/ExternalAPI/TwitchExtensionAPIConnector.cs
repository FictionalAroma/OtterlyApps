using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using Newtonsoft.Json;
using Otterly.API.ClientLib;
using Otterly.API.ExternalAPI.Objects;
using JsonClaimValueTypes = Microsoft.IdentityModel.JsonWebTokens.JsonClaimValueTypes;

namespace Otterly.API.ExternalAPI;

public class TwitchExtensionAPIConnector : FactoryAPIClientBase
{
	private readonly TwitchExtensionConfiguration _config;

	private const string BaseURL = "https://api.twitch.tv/helix/";
	public TwitchExtensionAPIConnector(HttpClient client, IConfiguration config) : base(client)
	{
		_config = config.GetSection("Twitch:Bingo").Get<TwitchExtensionConfiguration>();
	}

	public string GenerateJWT(string sendingUserID, string channelID)
	{
		var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_config.Secret));
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
		var sendClaim = new { send = new[] { "broadcast" } };
		
		var claims = new List<Claim>()
					 {
						 new Claim("role", "external"),
						 new Claim("user_id", sendingUserID),
						 new Claim("channel_id", channelID),
						 new Claim("pubsub_perms", JsonConvert.SerializeObject(sendClaim), JsonClaimValueTypes.Json ),
					 };
		var token = new JwtSecurityToken(claims: claims,
										 expires: DateTime.Now.AddMinutes(15),
										 signingCredentials: credentials);
		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public async Task<bool> SendExtensionMessage<T>(T messageToSend, string broadcastTwitchID)
	{
		try
		{
			var message = JsonConvert.SerializeObject(messageToSend);
			var payload = new TwitchExtensionPubSubMessage()
						  {
							  BroadcasterId = broadcastTwitchID,
							  Message = JsonConvert.SerializeObject(messageToSend)
						  };
			var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseURL}extensions/pubsub")
						  {
							  Content = new StringContent(JsonConvert.SerializeObject(payload)),
						  };
			request.Headers.Add("Client-Id", _config.ClientID);
			Authentication = new AuthenticationHeaderValue("Bearer", GenerateJWT(broadcastTwitchID, broadcastTwitchID));
			await ProcessRequest(request);
			return true;
		}
		catch (Exception e)
		{
			return false;
		}

	}
}