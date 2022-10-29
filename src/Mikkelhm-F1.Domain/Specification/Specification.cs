using System;
using System.Linq.Expressions;

namespace Mikkelhm_F1.Domain.Specification;

public abstract class Specification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Criteria { get; }

    public Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
    }
}