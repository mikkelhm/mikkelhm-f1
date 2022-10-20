using Azure.Core;
using Azure.Identity;
using Microsoft.Extensions.Configuration;

namespace Mikkelhm_F1.Infrastructure.Installers
{
    public static class AzureAppConfigurationInstaller
    {
        public static IConfigurationBuilder InstallAzureAppConfiguration(this IConfigurationBuilder config, string applicationRootPath)
        {
            var tempConfiguration = new ConfigurationBuilder()
                .SetBasePath(applicationRootPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var credentials = new DefaultAzureCredential(new DefaultAzureCredentialOptions());
            var appConfigEndpoint = tempConfiguration[Constants.EnvironmentVariableNames.AppConfigurationEndpoint];
            if (string.IsNullOrEmpty(appConfigEndpoint))
                throw new InvalidOperationException($"Azure App Configuration endpoint is null or empty string. Key: {Constants.EnvironmentVariableNames.AppConfigurationEndpoint} ");
            config.InternalAddAzureAppConfiguration(appConfigEndpoint, credentials);
            return config;
        }

        private static IConfigurationBuilder InternalAddAzureAppConfiguration(this IConfigurationBuilder config,
            string endpoint, TokenCredential credential)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
                throw new ArgumentNullException(nameof(endpoint));

            if (credential == null)
                throw new ArgumentNullException(nameof(credential));

            config.AddAzureAppConfiguration(options =>
            {
                // app configuration - load configuration key value pairs
                options.Connect(new Uri(endpoint), credential)
                        .Select("*")

                    // app configuration - load key vault references
                    .ConfigureKeyVault(kv => { kv.SetCredential(credential); })
                        .Select("*")

                    .ConfigureClientOptions(o =>
                    {
                        o.Diagnostics.IsLoggingEnabled = true;
                    });
            }, optional: false);

            return config;
        }
    }
}
