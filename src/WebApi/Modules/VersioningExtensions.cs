using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules
{
    /// <summary>
    /// Versioning Extensions.
    /// </summary>
    public static class VersioningExtensions
    {
        /// <summary>
        /// Method that adds versioning to the api.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services
                .AddApiVersioning(setupAction: apiVersioningOptions =>
                {
                    apiVersioningOptions.ReportApiVersions = true;
                    apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
                    apiVersioningOptions.DefaultApiVersion = new ApiVersion(1, 0);
                })
                .AddVersionedApiExplorer(setupAction: apiVersioningOptions =>
                {
                    apiVersioningOptions.GroupNameFormat = "'v'VVV";
                    apiVersioningOptions.SubstituteApiVersionInUrl = true;
                });
            return services;
        }
    }
}