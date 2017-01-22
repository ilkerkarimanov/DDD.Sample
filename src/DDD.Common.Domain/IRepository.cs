using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.Common.Domain
{
    public interface IRepository<TEntity, TIdentity>
        where TEntity: Entity
        where TIdentity: Identity
    {
        Task Create(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task Update(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
        Task Delete(TEntity entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}
