using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Domain.Interface;

public interface ICircuitRepository
{
    Task<Circuit> GetById(string id, CancellationToken cancellationToken = default);
    Task<Circuit> GetByCircuitId(string curcuitId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Circuit>> GetAll(CancellationToken cancellationToken = default);
    Task Save(Circuit upgrade, CancellationToken cancellationToken = default);
    Task Delete(string id, CancellationToken cancellationToken = default);
}
