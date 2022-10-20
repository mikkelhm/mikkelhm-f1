using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mikkelhm_F1.Infrastructure.Installers
{
    public static class CosmosDbInstaller
    {
        public static void InstallCosmosDb(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceNameUrl = configuration[Constants.EnvironmentVariableNames.CosmosDbServiceEndpoint];
            var authKey = configuration[Constants.EnvironmentVariableNames.CosmosDbServiceAuthKey];
            CosmosClient client = new CosmosClient(serviceNameUrl, authKey);

            var database = CreateDatabaseIfNotExists(client);

            CreateContainerIfNotExists(database, Constants.CosmosDbSettings.Seasons.ContainerId, Constants.CosmosDbSettings.Seasons.PartitionKeyPath);
            CreateContainerIfNotExists(database, Constants.CosmosDbSettings.Races.ContainerId, Constants.CosmosDbSettings.Races.PartitionKeyPath);

            services.AddSingleton<CosmosClient>(client);
        }
        private static Database CreateDatabaseIfNotExists(CosmosClient client)
        {
            var response = client.CreateDatabaseIfNotExistsAsync(Constants.CosmosDbSettings.DatabaseId,
                ThroughputProperties.CreateAutoscaleThroughput(1000)).Result;
            return response.Database;
        }

        private static Container CreateContainerIfNotExists(Database database, string containerId, string partitionKey)
        {
            var response = database.CreateContainerIfNotExistsAsync(containerId
                , partitionKey
                ).Result;
            return response.Container;
        }
    }
}
