using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Mikkelhm_F1.Domain.Interface;
using Mikkelhm_F1.Domain.Specification;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;

namespace Mikkelhm_F1.Infrastructure.Storage;

public abstract class CosmosDbBaseRepository<T> : IRepository<T>
{
    public CosmosDbBaseRepository(Container container)
    {
        Container = container ?? throw new ArgumentNullException(nameof(container));
    }

    protected Container Container { get; }

    public async Task<IEnumerable<T>> Find(ISpecification<T> specification, CancellationToken cancellationToken)
    {
        if (specification is null) throw new ArgumentNullException(nameof(specification));

        return await FindInternal(specification.Criteria, cancellationToken);
    }

    protected async Task<T?> GetByIdInternal(string id, string partitionKey, CancellationToken cancellationToken)
    {
        ItemResponse<T> response;
        try
        {
            response = await Container.ReadItemAsync<T>(id, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
        }
        catch (CosmosException e)
        {
            if (e.StatusCode == HttpStatusCode.NotFound)
                return default;

            throw;
        }

        return response.Resource;
    }

    protected Task SaveInternal(T entity, string partitionKey, CancellationToken cancellationToken)
    {
        return Container.UpsertItemAsync(entity, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
    }

    protected Task DeleteInternal(string id, string partitionKey, CancellationToken cancellationToken)
    {
        return Container.DeleteItemAsync<T>(id, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
    }

    protected async Task<T> FindOneInternal(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken)
    {
        var query = Container.GetItemLinqQueryable<T>()
            .Where(criteria);

        return await ExecuteQueryAsync(query, cancellationToken).SingleOrDefaultAsync(cancellationToken);
    }

    protected async Task<IEnumerable<T>> FindInternal(Expression<Func<T, bool>> criteria, CancellationToken cancellationToken)
    {
        var query = Container.GetItemLinqQueryable<T>()
            .Where(criteria);

        return await ExecuteQueryAsync(query, cancellationToken).ToListAsync(cancellationToken);
    }

    protected async IAsyncEnumerable<T> ExecuteQueryAsync(IQueryable<T> query, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using var feedIterator = query.ToFeedIterator();

        while (feedIterator.HasMoreResults)
        {
            cancellationToken.ThrowIfCancellationRequested();

            foreach (var entity in await feedIterator.ReadNextAsync(cancellationToken))
                yield return entity;
        }
    }
}