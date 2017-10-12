namespace Inventory.Server
{
    using System.IO;

    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    ///
    /// </summary>
    public static class Program
    {
        public static void Main(string[] args)
        {
            // Disable ApplicationInsight
            TelemetryConfiguration.Active.DisableTelemetry = true;

            BuildWebHost(args).Run();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hosting.json", optional: true)
                .AddCommandLine(args)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(true) // Capture Startup Errors
                //.UseSetting(WebHostDefaults.DetailedErrorsKey, "true")  // Detailed Errors
                .UseConfiguration(config)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
