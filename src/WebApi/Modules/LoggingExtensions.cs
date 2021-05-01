using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApi.Modules
{
    /// <summary>
    /// Logging Extensions.
    /// </summary>
    public static class LoggingExtensions
    {
        /// <summary>
        /// Add Invalid Request Logging dependencies.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInvalidRequestLogging(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(apiBehaviorOptions =>
            {
                apiBehaviorOptions.InvalidModelStateResponseFactory = actionContext =>
                {
                    var logger = actionContext.HttpContext.RequestServices.GetRequiredService<ILogger<Startup>>();
                    var errors = actionContext.ModelState.Values.SelectMany(modelStateEntry => modelStateEntry.Errors).Select(modelError => modelError.ErrorMessage).ToList();
                    var jsonModelState = JsonSerializer.Serialize(value: errors);
                    logger.LogWarning(message: "Invalid request.", args: jsonModelState);
                    var problemDetails = new ValidationProblemDetails(modelState: actionContext.ModelState);
                    return new BadRequestObjectResult(error: problemDetails);
                };
            });
            return services;
        }
    }
}