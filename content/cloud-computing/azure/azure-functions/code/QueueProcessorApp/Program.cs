using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QueueProcessorApp.Configuration;
using QueueProcessorApp.Data;
using QueueProcessorApp.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((context, configBuilder) =>
    {
        var environment = context.HostingEnvironment.EnvironmentName;
        
        configBuilder
            .SetBasePath(context.HostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        var environment = context.HostingEnvironment.EnvironmentName;

        // Database configuration
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Add logging
        services.AddApplicationInsightsTelemetry();
        services.AddLogging(config =>
        {
            config.AddConsole();
            config.SetMinimumLevel(GetLogLevel(environment));
        });

        // Register services
        services.AddScoped<IQueueProcessorService, QueueProcessorService>();
        services.AddScoped<IDataService, DataService>();

        // Configuration services
        services.Configure<FunctionSettings>(configuration.GetSection("FunctionSettings"));
        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

        // Add HttpClient for external APIs if needed
        services.AddHttpClient();
    })
    .Build();

host.Run();

static LogLevel GetLogLevel(string environment) =>
    environment switch
    {
        "Development" => LogLevel.Debug,
        "Staging" => LogLevel.Information,
        "Production" => LogLevel.Warning,
        _ => LogLevel.Information
    };
