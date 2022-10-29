using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mikkelhm_F1.Infrastructure.Installers;
using Mikkelhm_F1.SyncFunctions;
using Mikkelhm_F1.SyncFunctions.Syncronization;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Mikkelhm_F1.SyncFunctions;

public class Startup : FunctionsStartup
{
    private IConfigurationRoot _config;
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        _config = builder.ConfigurationBuilder
            .SetBasePath(builder.GetContext().ApplicationRootPath)
            .AddJsonFile($"appsettings.json", false)
            .InstallAzureAppConfiguration(builder.GetContext().ApplicationRootPath)
            .Build();
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        // Infrastructure
        ServicesInstaller.Install(builder.Services);


        builder.Services.AddTransient<IDataSyncronizer, DataSyncOrchestrator>();
        builder.Services.AddHttpClient();

        var provider = builder.Services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();
        builder.Services.InstallCosmosDb(configuration);

        var instrumentationKey = configuration[Infrastructure.Constants.EnvironmentVariableNames.ApplicationInsightsInstrumentationKey];
        if (string.IsNullOrEmpty(instrumentationKey) == false)
            builder.Services.AddApplicationInsightsTelemetry(instrumentationKey);

    }
}
