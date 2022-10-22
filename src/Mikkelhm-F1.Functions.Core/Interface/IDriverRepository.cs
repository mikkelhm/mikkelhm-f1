using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Domain.Interface;

public interface IDriverRepository
{
    Task<Driver> GetById(string id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Driver>> GetAll(CancellationToken cancellationToken = default);
    Task Save(Driver upgrade, CancellationToken cancellationToken = default);
    Task Delete(string id, CancellationToken cancellationToken = default);
}
