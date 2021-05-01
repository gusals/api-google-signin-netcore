using System;
using System.Threading.Tasks;
using Application.Services;
using Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules
{
    /// <summary>
    /// Authentication Extensions.
    /// </summary>
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// Add Security dependencies.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var signingKey = configuration.GetValue<string>("AuthenticationModule:SigningKey");
            var audience = configuration.GetValue<string>("AuthenticationModule:Audience");
            var issuer = configuration.GetValue<string>("AuthenticationModule:Issuer");
            var expires = 20d;

            void JwtBearer(JwtBearerOptions jwtBearerOptions)
            {
                jwtBearerOptions.TokenValidationParameters = services.BuildServiceProvider().GetRequiredService<JsonWebTokenSettings>().TokenValidationParameters();
                jwtBearerOptions.Events.OnMessageReceived = messageReceivedContext =>
                {
                    if (messageReceivedContext.Request.Cookies.ContainsKey("X-Access-Token"))
                        messageReceivedContext.Token = messageReceivedContext.Request.Cookies["X-Access-Token"];
                    return Task.CompletedTask;
                };
            }

            services
                .AddJsonWebToken(
                    securityKey: signingKey,
                    expires: TimeSpan.FromMinutes(value: expires),
                    issuer: issuer)
                .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearer);
            return services;
        }

        private static IServiceCollection AddJsonWebToken(this IServiceCollection services, string securityKey, TimeSpan expires, string issuer) =>
            services.AddJsonWebToken(jsonWebTokenSettings: new JsonWebTokenSettings(securityKey: securityKey, expires: expires, issuer: issuer));

        private static IServiceCollection AddJsonWebToken(this IServiceCollection services, JsonWebTokenSettings jsonWebTokenSettings) =>
            services
                .AddSingleton(implementationFactory: _ => jsonWebTokenSettings)
                .AddSingleton<ITokenService, TokenService>();
    }
}