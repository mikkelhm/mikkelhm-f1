using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Domain.Interface;

public interface IRaceRepository
{
    Task<Race> GetById(string id, CancellationToken cancellationToken = default);
    Task<Race> GetByYear(int year, CancellationToken cancellationToken = default);
    Task<IEnumerable<Race>> GetAll(CancellationToken cancellationToken = default);
    Task Save(Race upgrade, CancellationToken cancellationToken = default);
    Task Delete(string id, CancellationToken cancellationToken = default);
}
