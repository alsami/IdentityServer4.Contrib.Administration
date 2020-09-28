using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace IdentityServer4.Contrib.Administration
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog(ConfigureLogger)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }

        private static void ConfigureLogger(HostBuilderContext hostBuilderContext,
            LoggerConfiguration loggerConfiguration)
            => loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
    }
}