using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace todo_application
{
    public static class Program
    {
        public static IConfiguration configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(path: "appSettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            #pragma warning disable S1075 // URIs should not be hardcoded
            string path = @"c:\temp\logs\todo-application.json";
            #pragma warning restore S1075 // URIs should not be hardcoded

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.File(new JsonFormatter(), path, shared: true)
                .CreateLogger();
                

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
