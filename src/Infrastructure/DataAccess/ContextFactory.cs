using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.DataAccess
{
    /// <inheritdoc />
    public sealed class ContextFactory : IDesignTimeDbContextFactory<MemberContext>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns><see cref="MemberContext"/></returns>
        public MemberContext CreateDbContext(string[] args)
        {
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<MemberContext>();
            dbContextOptionsBuilder
                .UseMySql(
                    connectionString: ReadContentConnectionStringFromAppSettings(),
                    serverVersion: new MySqlServerVersion(version: new Version(8, 0, 21)))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            return new MemberContext(options: dbContextOptionsBuilder.Options);
        }

        private static string ReadContentConnectionStringFromAppSettings()
        {
            var environmentVariable = Environment.GetEnvironmentVariable(variable: "ASPNETCORE_ENVIRONMENT");
            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(basePath: Path.Combine(paths: Directory.GetCurrentDirectory()))
                .AddJsonFile(path: "appsettings.json", optional: false)
                .AddJsonFile(path: $"appsettings.{environmentVariable}.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
            return configurationRoot.GetValue<string>(key: "DataAccessModule:MemberConnection");
        }
    }
}