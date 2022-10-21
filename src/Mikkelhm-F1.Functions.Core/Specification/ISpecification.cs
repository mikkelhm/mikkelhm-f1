using System;
using System.Linq.Expressions;

namespace Mikkelhm_F1.Domain.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}
