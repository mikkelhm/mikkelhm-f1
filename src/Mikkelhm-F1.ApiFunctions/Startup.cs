using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mikkelhm_F1.ApiFunctions;
using Mikkelhm_F1.Infrastructure.Installers;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Mikkelhm_F1.ApiFunctions;

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
        ServicesInstaller.InstallInfrastructure(builder.Services);

        builder.Services.AddHttpClient();

        var provider = builder.Services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();
        builder.Services.InstallCosmosDb(configuration);

        var instrumentationKey = configuration[Infrastructure.Constants.EnvironmentVariableNames.ApplicationInsightsInstrumentationKey];
        if (string.IsNullOrEmpty(instrumentationKey) == false)
            builder.Services.AddApplicationInsightsTelemetry(instrumentationKey);

    }
}
