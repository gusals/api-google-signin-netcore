using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApi.Modules
{
    /// <summary>
    /// HealthCheck Extensions.
    /// </summary>
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// Add Health Checks dependencies varying on configuration.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddDbContextCheck<MemberContext>();
            return services;
        }

        /// <summary>
        /// Use Health Checks dependencies.
        /// </summary>
        /// <param name="application">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <returns><see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder application)
        {
            application.UseHealthChecks(path: "/health", options: new HealthCheckOptions { ResponseWriter = ResponseWriter });
            return application;
        }

        private static Task ResponseWriter(HttpContext context, HealthReport report)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            var json = new JObject(
                new JProperty(name: "status", content: report.Status),
                new JProperty(name: "results",
                    content: new JObject(content: report.Entries.Select(entry =>
                        new JProperty(name: entry.Key,
                            content: new JObject(
                                new JProperty(name: "status", content: entry.Value.Status),
                                new JProperty(name: "description", content: entry.Value.Description),
                                new JProperty(name: "data", content: new JObject(content: entry.Value.Data.Select(pair => new JProperty(pair.Key, pair.Value))))))))));
            return context.Response.WriteAsync(text: json.ToString(formatting: Formatting.Indented));
        }
    }
}