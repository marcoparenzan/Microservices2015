using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models
{
    public interface IRepository<TAggregateRoot, TId>
        where TAggregateRoot: IAggregateRoot<TId>
    {
        Task<TAggregateRoot> GetByIdAsync(TId id);
        Task SetAsync(TAggregateRoot aggregateRoot);
        TAggregateRoot GetById(TId id);
        void Set(TAggregateRoot aggregateRoot);
    }
}
