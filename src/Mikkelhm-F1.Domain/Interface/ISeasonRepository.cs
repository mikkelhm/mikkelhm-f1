using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mikkelhm_F1.Domain.Interface;

public interface ISeasonRepository
{
    Task<Season> GetById(string id, CancellationToken cancellationToken = default);
    Task<Season> GetByYear(int year, CancellationToken cancellationToken = default);
    Task<IEnumerable<Season>> GetAll(CancellationToken cancellationToken = default);
    Task Save(Season upgrade, CancellationToken cancellationToken = default);
    Task Delete(string id, CancellationToken cancellationToken = default);
}
