
using OS.Persistence;
using Serilog;
using Serilog.Events;
using System.Net;

namespace OS.Webapi
{
    public class Program
    {

        // Configure the HTTP request pipeline.

        // dotnet ef database update  --project OS.Persistence/OS.Persistence.csproj   --startup-project OS.Webapi/OS.Webapi.csproj
        // dotnet ef migrations add Initial --project OS.Persistence/OS.Persistence.csproj   --startup-project OS.Webapi/OS.Webapi.csproj

       
        // Ctrl+Shift+I - Reformat (Windows)
        // dotnet publish OS.Webapi/OS.Webapi.csproj

        // dotnet run --project OS.Webapi/OS.Webapi.csproj seed 





        public static async Task Main(string[] args)
        {
            string fileName = Path.Combine("Logs", "OSWebapi-.log");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.Console()
                .WriteTo.File(fileName, rollingInterval: RollingInterval.Day)
                .CreateLogger();
            var host = CreateHostBuilder(args).Build();

            bool seed = false;
            if (args.Length > 0)
            {
                seed = args.Any(x => x == "seed");
            }

            if (seed)
            {
                using (var scope = host.Services.CreateScope())
                {
                    var serviceProvider = scope.ServiceProvider;
                    try
                    {
                        var configManager = WebApplication.CreateBuilder(args).Configuration;
                        await DbInitializer.RunSeed(configManager, serviceProvider);

                    }
                    catch (Exception exception)
                    {
                        Log.Fatal(exception, "An error occurred while app initialization");
                    }
                }
            }
         
            else
            {
                await host.RunAsync();
            }
        }



        public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
          .UseSerilog()
          .ConfigureWebHostDefaults(webBuilder =>
          { 
              webBuilder.UseStartup<Startup>();
          });

    }
}
