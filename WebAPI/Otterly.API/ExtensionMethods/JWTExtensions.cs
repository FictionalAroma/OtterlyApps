using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Otterly.API.ExtensionMethods;

public static class JWTExtensions
{
	public static Claim? GetClaim(this IEnumerable<Claim> claims, string key)
	{
		return claims.FirstOrDefault(c => c.Type == key);
	}

	public static string GetClaimValue(this IEnumerable<Claim> claims, string key)
	{
		return claims.GetClaim(key)?.Value ?? string.Empty;
	}

}