using Mikkelhm_F1.Domain.Specification;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Domain.Interface
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Find(ISpecification<T> specification, CancellationToken cancellationToken = default);
    }
}
