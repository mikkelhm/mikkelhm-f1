using Microsoft.Azure.Cosmos;
using Mikkelhm_F1.Domain;
using Mikkelhm_F1.Domain.Interface;

namespace Mikkelhm_F1.Infrastructure.Storage;

public class CosmosDbDriverRepository : CosmosDbBaseRepository<Driver>, IDriverRepository
{
    public CosmosDbDriverRepository(Container container) : base(container)
    {
    }

    public async Task Delete(string id, CancellationToken cancellationToken = default)
    {
        await DeleteInternal(id, Driver.PartitionKeyValue, cancellationToken);
    }

    public async Task<Driver> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await GetByIdInternal(id, Driver.PartitionKeyValue, cancellationToken);
    }

    public async Task<IEnumerable<Driver>> GetAll(CancellationToken cancellationToken = default)
    {
        return await FindInternal(x => x.PartitionKey == Driver.PartitionKeyValue, cancellationToken);
    }

    public async Task Save(Driver Driver, CancellationToken cancellationToken = default)
    {
        await SaveInternal(Driver, Driver.PartitionKeyValue, cancellationToken);
    }
}
