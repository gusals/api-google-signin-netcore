using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus;
using WebApi.Modules;
using Westwind.AspNetCore.LiveReload;

namespace WebApi
{
    /// <summary>
    /// Startup.
    /// </summary>
    public sealed class Startup
    {
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Startup Constructor.
        /// </summary>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        public Startup(IConfiguration configuration) => Configuration = configuration;

        /// <summary>
        /// Configure dependencies from application.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        public void ConfigureServices(IServiceCollection services) =>
            services
                .AddResponseCompression()
                .AddInvalidRequestLogging()
                .AddMySql(configuration: Configuration)
                .AddHealthChecks(configuration: Configuration)
                .AddAuthentication(configuration: Configuration)
                .AddVersioning()
                .AddSwagger()
                .AddUseCases()
                .AddCustomControllers()
                .AddCustomCors();

        /// <summary>
        /// Configure http request pipeline.
        /// </summary>
        /// <param name="application">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <param name="webHostEnvironment">Provides information about the web hosting environment an application is running in.</param>
        /// <param name="apiVersionDescriptionProvider">Defines the behavior of a provider that discovers and describes API version information within an application.</param>
        public void Configure(IApplicationBuilder application, IWebHostEnvironment webHostEnvironment, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (webHostEnvironment.IsDevelopment())
                application
                    .UseDeveloperExceptionPage()
                    .UseLiveReload()
                    .UseVersionedSwagger(apiVersionDescriptionProvider: apiVersionDescriptionProvider, configuration: Configuration, webHostEnvironment: webHostEnvironment);
            else
                application.UseHsts();

            application
                .UseResponseCompression()
                .UseHealthChecks()
                .UseCustomCors()
                .UseHttpMetrics()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpointRouteBuilder =>
                {
                    endpointRouteBuilder.MapControllers();
                    endpointRouteBuilder.MapMetrics();
                });
        }
    }
}