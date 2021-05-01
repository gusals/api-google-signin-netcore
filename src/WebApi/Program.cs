using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    /// <summary>
    /// Program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) => CreateHostBuilder(args: args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args: args)
                .ConfigureAppConfiguration(configureDelegate: (hostBuilderContext, configurationBuilder) => configurationBuilder.AddCommandLine(args: args))
                .ConfigureWebHostDefaults(configure: webHostBuilder => webHostBuilder.UseStartup<Startup>());
    }
}