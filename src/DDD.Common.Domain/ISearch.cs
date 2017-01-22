using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DDD.Common.Domain
{
    public interface ISearch<TEntity, in TIdentity> 
        where TEntity : Entity
        where TIdentity: Identity
    {
        // TODO: IList<string> criteria to be extended with more metadata.
        Task<IEnumerable<TEntity>> Search(IList<string> criteria, CancellationToken cancellationToken = default(CancellationToken));
    }
}
