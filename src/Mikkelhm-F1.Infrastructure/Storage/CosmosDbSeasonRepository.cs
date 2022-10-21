using Microsoft.Azure.Cosmos;
using Mikkelhm_F1.Domain;
using Mikkelhm_F1.Domain.Interface;

namespace Mikkelhm_F1.Infrastructure.Storage;

public class CosmosDbSeasonRepository : CosmosDbBaseRepository<Season>, ISeasonRepository
{
    public CosmosDbSeasonRepository(Container container) : base(container)
    {
    }

    public async Task Delete(string id, CancellationToken cancellationToken = default)
    {
        await DeleteInternal(id, Season.PartitionKeyValue, cancellationToken);
    }

    public async Task<Season> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await GetByIdInternal(id, Season.PartitionKeyValue, cancellationToken);
    }

    public async Task<Season> GetByYear(int year, CancellationToken cancellationToken = default)
    {
        return await FindOneInternal(x => x.PartitionKey == Season.PartitionKeyValue && x.Year == year, cancellationToken);
    }

    public async Task<IEnumerable<Season>> GetAll(CancellationToken cancellationToken = default)
    {
        return await FindInternal(x => x.PartitionKey == Season.PartitionKeyValue, cancellationToken);
    }

    public async Task Save(Season season, CancellationToken cancellationToken = default)
    {
        await SaveInternal(season, Season.PartitionKeyValue, cancellationToken);
    }
}
