using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Tokens
{
    /// <summary>
    /// JsonWebTokenSettings.
    /// </summary>
    public sealed class JsonWebTokenSettings
    {
        /// <summary>
        /// JsonWebTokenSettings Constructor.
        /// </summary>
        /// <param name="securityKey">서명 키.</param>
        /// <param name="expires">토큰 만료시간.</param>
        public JsonWebTokenSettings(string securityKey, TimeSpan expires)
        {
            SecurityKey = new SymmetricSecurityKey(key: Encoding.Default.GetBytes(s: securityKey));
            Expires = expires;
        }

        /// <summary>
        /// JsonWebTokenSettings Constructor.
        /// </summary>
        /// <param name="securityKey">서명 키.</param>
        /// <param name="expires">토큰 만료시간.</param>
        /// <param name="issuer">토큰 발급자.</param>
        public JsonWebTokenSettings(string securityKey, TimeSpan expires, string issuer) : this(securityKey: securityKey, expires: expires)
        {
            SecurityKey = new SymmetricSecurityKey(key: Encoding.Default.GetBytes(s: securityKey));
            Expires = expires;
            Issuer = issuer;
        }

        /// <summary>
        /// JsonWebTokenSettings Constructor.
        /// </summary>
        /// <param name="securityKey">서명 키.</param>
        /// <param name="expires">토큰 만료시간.</param>
        /// <param name="audience">토큰 대상자.</param>
        /// <param name="issuer">토큰 발급자.</param>
        public JsonWebTokenSettings(string securityKey, TimeSpan expires, string audience, string issuer) : this(securityKey: securityKey, expires: expires)
        {
            Audience = audience;
            Issuer = issuer;
        }

        /// <summary>
        /// 서명 키.
        /// </summary>
        public SecurityKey SecurityKey { get; }

        /// <summary>
        /// 토큰 만료시간.
        /// </summary>
        public TimeSpan Expires { get; }

        /// <summary>
        /// 토큰 대상자.
        /// </summary>
        public string Audience { get; } = string.Empty;

        /// <summary>
        /// 토큰 발급자.
        /// </summary>
        public string Issuer { get; } = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TokenValidationParameters TokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                IssuerSigningKey = SecurityKey,
                ValidAudience = Audience,
                ValidIssuer = Issuer,
                ValidateAudience = !string.IsNullOrEmpty(Audience),
                ValidateIssuer = !string.IsNullOrEmpty(Issuer),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}