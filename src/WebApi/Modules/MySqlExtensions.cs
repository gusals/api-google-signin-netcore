using System;
using Application.Services;
using Domain.Members;
using Domain.RefreshTokens;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Factories;
using Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Modules
{
    /// <summary>
    /// MySql Extensions.
    /// </summary>
    public static class MySqlExtensions
    {
        /// <summary>
        /// Add MySql dependencies varying on configuration.
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
        /// <param name="configuration">Represents a set of key/value application configuration properties.</param>
        /// <returns><see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddMySql(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<MemberContext>(dbContextOptionsBuilder =>
                    dbContextOptionsBuilder.UseMySql(
                        connectionString: configuration.GetValue<string>("DataAccessModule:MemberConnection"),
                        serverVersion: new MySqlServerVersion(version: new Version(8, 0, 21))))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IMemberFactory, MemberFactory>()
                .AddScoped<IMemberRepository, MemberRepository>()
                .AddScoped<IRefreshTokenFactory, RefreshTokenFactory>()
                .AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }
}