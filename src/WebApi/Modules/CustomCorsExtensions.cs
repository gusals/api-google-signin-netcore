using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules
{
    /// <summary>
    /// Custom Cors Extensions.
    /// </summary>
    public static class CustomCorsExtensions
    {
        private const string ALLOWS_ANY = "_allowsAny";

        /// <summary>
        /// Add CORS dependencies.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCustomCors(this IServiceCollection services) =>
            services.AddCors(setupAction: corsOptions =>
                corsOptions.AddPolicy(
                    name: ALLOWS_ANY,
                    configurePolicy: corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        /// <summary>
        /// Use CORS dependencies.
        /// </summary>
        /// <param name="application">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <returns><see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseCustomCors(this IApplicationBuilder application) => application.UseCors(policyName: ALLOWS_ANY);
    }
}