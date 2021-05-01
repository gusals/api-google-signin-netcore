using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebApi.Modules
{
    /// <summary>
    /// Swagger Extensions.
    /// </summary>
    public static class SwaggerExtensions
    {
        private static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = $"{typeof(Startup).GetTypeInfo().Assembly.GetName().Name}.xml";
                return Path.Combine(path1: basePath, path2: fileName);
            }
        }

        /// <summary>
        /// Add Swagger Configuration dependencies.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services
                .AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>()
                .AddSwaggerGen(swaggerGenOptions =>
                {
                    swaggerGenOptions.IncludeXmlComments(filePath: XmlCommentsFilePath);
                    swaggerGenOptions.AddSecurityDefinition(
                        name: "Bearer",
                        securityScheme: new OpenApiSecurityScheme
                        {
                            In = ParameterLocation.Header,
                            Description = "Please insert JWT with Bearer into field",
                            Name = "Authorization",
                            Type = SecuritySchemeType.ApiKey
                        });
                    swaggerGenOptions.AddSecurityRequirement(
                        securityRequirement: new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                                Array.Empty<string>()
                            }
                        });
                    swaggerGenOptions.SchemaFilter<EnumDescriptorSchemaFilter>();
                    swaggerGenOptions.OrderActionsBy(sortKeySelector: apiDescription => $"{apiDescription.RelativePath}");
                });

        /// <summary>
        /// Add Swagger dependencies.
        /// </summary>
        /// <param name="application">Defines a class that provides the mechanisms to configure an application's request pipeline.</param>
        /// <param name="apiVersionDescriptionProvider">Defines the behavior of a provider that discovers and describes API version information within an application.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <param name="webHostEnvironment">Provides information about the web hosting environment an application is running in.</param>
        /// <returns><see cref="IApplicationBuilder"/></returns>
        public static IApplicationBuilder UseVersionedSwagger(
            this IApplicationBuilder application,
            IApiVersionDescriptionProvider apiVersionDescriptionProvider,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment) =>
                application
                    .UseSwagger()
                    .UseSwaggerUI(swaggerUIOptions =>
                    {
                        foreach (var apiVersionDescription in apiVersionDescriptionProvider.ApiVersionDescriptions)
                        {
                            var basePath = configuration[key: "ASPNETCORE_BASEPATH"];
                            var swaggerEndpoint = !string.IsNullOrEmpty(value: basePath)
                                ? $"{basePath}/swagger/{apiVersionDescription.GroupName}/swagger.json"
                                : $"/swagger/{apiVersionDescription.GroupName}/swagger.json";
                            swaggerUIOptions.SwaggerEndpoint(url: swaggerEndpoint, name: apiVersionDescription.GroupName.ToUpperInvariant());
                            swaggerUIOptions.DocExpansion(docExpansion: DocExpansion.None);
                        }
                    });
    }
}