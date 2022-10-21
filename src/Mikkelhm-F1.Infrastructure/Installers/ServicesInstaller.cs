using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Mikkelhm_F1.Domain.Interface;
using Mikkelhm_F1.Infrastructure.Storage;

namespace Mikkelhm_F1.Infrastructure.Installers;

public class ServicesInstaller
{
    public static void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ISeasonRepository>(c =>
            new CosmosDbSeasonRepository(c.GetRequiredService<CosmosClient>().GetContainer(
                Constants.CosmosDbSettings.DatabaseId,
                Constants.CosmosDbSettings.Seasons.ContainerId)));
    }
}
