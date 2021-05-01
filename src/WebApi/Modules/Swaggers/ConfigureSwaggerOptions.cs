using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Modules
{
    /// <summary>
    /// Configures the Swagger generation options.
    /// </summary>
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private const string TERM_OF_SERVICE_URI = "https://gusals.github.io/service";
        private const string LICENSE = "https://gusals.github.io/license";

        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerOptions" /> class.
        /// </summary>
        /// <param name="apiVersionDescriptionProvider">Defines the behavior of a provider that discovers and describes API version information within an application.</param>
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider) => _apiVersionDescriptionProvider = apiVersionDescriptionProvider;

        /// <summary>
        /// Add a swagger document for each discovered API version.
        /// </summary>
        /// <param name="swaggerGenOptions"></param>
        public void Configure(SwaggerGenOptions swaggerGenOptions)
        {
            foreach (var apiVersionDescriptions in _apiVersionDescriptionProvider.ApiVersionDescriptions)
                swaggerGenOptions.SwaggerDoc(name: apiVersionDescriptions.GroupName, info: CreateInfoForApiVersion(apiVersionDescription: apiVersionDescriptions));
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription apiVersionDescription)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = "Gusals API",
                Version = apiVersionDescription.ApiVersion.ToString(),
                Description = string.Empty,
                Contact = new OpenApiContact { Name = "Gusals", Email = "gusals@gmail.com" },
                TermsOfService = new Uri(uriString: TERM_OF_SERVICE_URI),
                License = new OpenApiLicense { Name = "License", Url = new Uri(uriString: LICENSE) }
            };
            if (apiVersionDescription.IsDeprecated)
                openApiInfo.Description += " This API version has been deprecated.";
            return openApiInfo;
        }
    }
}