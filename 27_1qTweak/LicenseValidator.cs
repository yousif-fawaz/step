using System;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace _27_1qTweak
{
	public static class LicenseValidator
	{
		public static string? Load(string path) => File.Exists(path) ? File.ReadAllText(path, Encoding.UTF8) : null;

		public static bool Verify(string jwt, string publicKeyPem, out JwtSecurityToken? token)
		{
			token = null;
			try
			{
				using var rsa = RSA.Create();
				rsa.ImportFromPem(publicKeyPem.AsSpan());
				var parms = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					RequireExpirationTime = true,
					ValidateLifetime = true,
					ClockSkew = TimeSpan.FromMinutes(2),
					IssuerSigningKey = new RsaSecurityKey(rsa),
					ValidateIssuerSigningKey = true
				};
				var handler = new JwtSecurityTokenHandler();
				handler.ValidateToken(jwt, parms, out var validated);
				token = (JwtSecurityToken)validated;
				return true;
			}
			catch { return false; }
		}

		public static bool IsValid(string jwt, string publicKeyPem)
		{
			if (!Verify(jwt, publicKeyPem, out var t)) return false;
			var ok = t!.Payload.TryGetValue("hw", out var hv);
			if (!ok) return false;
			var hw = hv?.ToString();
			return !string.IsNullOrWhiteSpace(hw) && hw!.Equals(HardwareFingerprint.Get(), StringComparison.OrdinalIgnoreCase);
		}
	}
}