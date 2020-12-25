namespace ConsoleTemplate_Net5
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Serilog;

    /// <summary>
    /// Static program bootstrap.
    /// </summary>
    internal static class Program
    {
        private const string APPSETTINGNAME = "appsettings.json";
        // private static readonly ILogger Logger = new Logger<Program>();

        /// <summary>
        /// Main app entry.
        /// </summary>
        /// <param name="args">start arguments.</param>
        /// <returns>async task of program entry.</returns>
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                .Console()
                .CreateLogger();
            Log.Logger.Information("::::::::::::::::::: Start :::::::::::::::::::");
            try
            {
                var config = GetConfiguration(APPSETTINGNAME);
                var servicesProvider = BuildServiceProvider(config);
                using (servicesProvider as IDisposable)
                {
                    var app = servicesProvider.GetRequiredService<App>();
                    await app.Run();
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex, "Stopped program because of exception");
                throw;
            }

            Log.Logger.Information("::::::::::::::::::: End :::::::::::::::::::");
        }

        private static IServiceProvider BuildServiceProvider(IConfiguration config)
        {
            return new ServiceCollection()
               .AddSingleton<App>()
               .AddLogging(loggingBuilder =>
               {
                   loggingBuilder
                       .ClearProviders()
                       .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
                       .AddSerilog();
               })
               .BuildServiceProvider();
        }

        private static IConfiguration GetConfiguration(string configName)
        {
            return new ConfigurationBuilder()
                   .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                   .AddJsonFile(configName, optional: true, reloadOnChange: true)
                   .Build();
        }
    }
}
