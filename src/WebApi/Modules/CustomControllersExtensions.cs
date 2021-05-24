using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Services;
using Infrastructure.HttpContexts;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Westwind.AspNetCore.LiveReload;

namespace WebApi.Modules
{
    /// <summary>
    /// Custom Controller Extensions.
    /// </summary>
    public static class CustomControllersExtensions
    {
        /// <summary>
        /// Add Custom Controller dependencies.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCustomControllers(this IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddScoped<IHttpContextService, HttpContextService>()
                .AddLiveReload()
                .AddRouting(routeOptions => routeOptions.LowercaseUrls = true)
                .AddMvc(mvcOptions =>
                {
                    mvcOptions.OutputFormatters.RemoveType<TextOutputFormatter>();
                    mvcOptions.OutputFormatters.RemoveType<StreamOutputFormatter>();
                    mvcOptions.RespectBrowserAcceptHeader = true;
                    //mvcOptions.Filters.Add(item: new ExceptionFilter());
                })
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.JsonSerializerOptions.AllowTrailingCommas = true;
                    jsonOptions.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    jsonOptions.JsonSerializerOptions.IgnoreNullValues = true;
                })
                .AddControllersAsServices();
            return services;
        }
    }
}