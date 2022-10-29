using Microsoft.Azure.Cosmos;
using Mikkelhm_F1.Domain;
using Mikkelhm_F1.Domain.Interface;

namespace Mikkelhm_F1.Infrastructure.Storage;

public class CosmosDbRaceRepository : CosmosDbBaseRepository<Race>, IRaceRepository
{
    public CosmosDbRaceRepository(Container container) : base(container)
    {
    }

    public async Task Delete(string id, CancellationToken cancellationToken = default)
    {
        await DeleteInternal(id, Race.PartitionKeyValue, cancellationToken);
    }

    public async Task<Race> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await GetByIdInternal(id, Race.PartitionKeyValue, cancellationToken);
    }

    public async Task<Race> GetByYear(int year, CancellationToken cancellationToken = default)
    {
        return await FindOneInternal(x => x.PartitionKey == Race.PartitionKeyValue && x.Year == year, cancellationToken);
    }

    public async Task<IEnumerable<Race>> GetAll(CancellationToken cancellationToken = default)
    {
        return await FindInternal(x => x.PartitionKey == Race.PartitionKeyValue, cancellationToken);
    }

    public async Task Save(Race Race, CancellationToken cancellationToken = default)
    {
        await SaveInternal(Race, Race.PartitionKeyValue, cancellationToken);
    }
}
