using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mikkelhm_F1.Infrastructure.Installers;

[assembly: FunctionsStartup(typeof(Mikkelhm_F1.Functions.Startup))]
namespace Mikkelhm_F1.Functions
{
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
            var provider = builder.Services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            builder.Services.InstallCosmosDb(configuration);
        }
    }
}
