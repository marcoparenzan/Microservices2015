using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Contracts
{
    public interface ITotem
    {
        Task<ITotem> NewPizzasToPrepareAsync(Guid commandId);
    }
}
