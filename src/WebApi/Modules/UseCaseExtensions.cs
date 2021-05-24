using Application.UseCases.OAuthSignin;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules
{
    /// <summary>
    /// Use-Case Extensions.
    /// </summary>
    public static class UseCaseExtensions
    {
        /// <summary>
        /// Add Use Case dependencies varying on configuration.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddUseCases(this IServiceCollection services) =>
            services.AddScoped<IOAuthSigninUseCase, OAuthSigninUseCase>();
    }
}