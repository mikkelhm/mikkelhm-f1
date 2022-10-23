using Microsoft.Azure.Cosmos;
using Mikkelhm_F1.Domain;
using Mikkelhm_F1.Domain.Interface;

namespace Mikkelhm_F1.Infrastructure.Storage;

public class CosmosDbCircuitRepository : CosmosDbBaseRepository<Circuit>, ICircuitRepository
{
    public CosmosDbCircuitRepository(Container container) : base(container)
    {
    }

    public async Task Delete(string id, CancellationToken cancellationToken = default)
    {
        await DeleteInternal(id, Circuit.PartitionKeyValue, cancellationToken);
    }

    public async Task<Circuit> GetById(string id, CancellationToken cancellationToken = default)
    {
        return await GetByIdInternal(id, Circuit.PartitionKeyValue, cancellationToken);
    }

    public async Task<Circuit> GetByCircuitId(string circuitId, CancellationToken cancellationToken = default)
    {
        return await FindOneInternal(x => x.PartitionKey == Circuit.PartitionKeyValue && x.CircuitId == circuitId, cancellationToken);
    }

    public async Task<IEnumerable<Circuit>> GetAll(CancellationToken cancellationToken = default)
    {
        return await FindInternal(x => x.PartitionKey == Circuit.PartitionKeyValue, cancellationToken);
    }

    public async Task Save(Circuit Circuit, CancellationToken cancellationToken = default)
    {
        await SaveInternal(Circuit, Circuit.PartitionKeyValue, cancellationToken);
    }

}
