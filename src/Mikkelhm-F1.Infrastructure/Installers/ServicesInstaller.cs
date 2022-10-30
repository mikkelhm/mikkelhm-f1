using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Mikkelhm_F1.Domain.Interface;
using Mikkelhm_F1.Infrastructure.Storage;

namespace Mikkelhm_F1.Infrastructure.Installers;

public class ServicesInstaller
{
    public static void InstallInfrastructure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISeasonRepository>(c =>
            new CosmosDbSeasonRepository(c.GetRequiredService<CosmosClient>().GetContainer(
                Constants.CosmosDbSettings.DatabaseId,
                Constants.CosmosDbSettings.Seasons.ContainerId)));

        serviceCollection.AddTransient<IDriverRepository>(c =>
            new CosmosDbDriverRepository(c.GetRequiredService<CosmosClient>().GetContainer(
                Constants.CosmosDbSettings.DatabaseId,
                Constants.CosmosDbSettings.Drivers.ContainerId)));

        serviceCollection.AddTransient<ICircuitRepository>(c =>
            new CosmosDbCircuitRepository(c.GetRequiredService<CosmosClient>().GetContainer(
                Constants.CosmosDbSettings.DatabaseId,
                Constants.CosmosDbSettings.Circuits.ContainerId)));

        serviceCollection.AddTransient<IRaceRepository>(c =>
            new CosmosDbRaceRepository(c.GetRequiredService<CosmosClient>().GetContainer(
                Constants.CosmosDbSettings.DatabaseId,
                Constants.CosmosDbSettings.Races.ContainerId)));
    }
}
