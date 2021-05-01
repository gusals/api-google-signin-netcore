using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Application.Services;
using Domain.ValueObjects;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Tokens
{
    /// <inheritdoc/>
    public sealed class TokenService : ITokenService
    {
        private const string BEARER_TOKEN_PREFIX = "Bearer ";
        private JsonWebTokenSettings JsonWebTokenSettings { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenService" /> class.
        /// </summary>
        /// <param name="jsonWebTokenSettings">JsonWebTokenSettings.</param>
        public TokenService(JsonWebTokenSettings jsonWebTokenSettings) => JsonWebTokenSettings = jsonWebTokenSettings;

        /// <inheritdoc/>
        public Token GenerateToken(string username, string name)
        {
            var accessToken = Encode(claims: GeneratePayload(username: username, name: name));
            var refreshToken = GenerateRefreshToken();

            var jsonWebToken = Decode(accessToken: accessToken);
            var issuedIn = GetClaimValue(jsonWebToken: jsonWebToken, key: "nbf");
            var expiresIn = GetClaimValue(jsonWebToken: jsonWebToken, key: "exp");

            return new Token(accessToken, refreshToken, double.Parse(issuedIn), double.Parse(expiresIn));
        }

        private string Encode(IList<Claim> claims)
        {
            var jwtSecurityToken = new JwtSecurityToken
            (
                issuer: JsonWebTokenSettings.Issuer,
                audience: JsonWebTokenSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(value: JsonWebTokenSettings.Expires),
                signingCredentials: new SigningCredentials(
                    key: JsonWebTokenSettings.SecurityKey,
                    algorithm: SecurityAlgorithms.HmacSha512Signature)
            );

            return new JwtSecurityTokenHandler().WriteToken(token: jwtSecurityToken);
        }

        private static Dictionary<string, object> Decode(string accessToken) => new JwtSecurityTokenHandler().ReadJwtToken(token: accessToken).Payload;

        private static IList<Claim> GeneratePayload(string username, string name) =>
            new List<Claim>
            {
                new Claim(type: "sub", value: username),
                new Claim(type: "name", value: name)
            };

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(data: randomNumber);
            return Convert.ToBase64String(inArray: randomNumber);
        }

        private static string GetClaimValue(Dictionary<string, object> jsonWebToken, string key)
        {
            var value = jsonWebToken.SingleOrDefault(jwt => jwt.Key.Equals(value: key, StringComparison.Ordinal)).Value.ToString();
            return !string.IsNullOrWhiteSpace(value: value) ? value : string.Empty;
        }

        private static string GetTokenValue(string accessToken)
        {
            if (accessToken.StartsWith(BEARER_TOKEN_PREFIX))
                accessToken = accessToken.Replace(BEARER_TOKEN_PREFIX, string.Empty);

            return accessToken;
        }
    }
}