using Call4Pizza.Models.Commands;
using Call4Pizza.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Contracts
{
    public interface ICommandHandler
    {
        Task<ICommandHandler> Handle(params EventEnvelope<OrderCreated>[] e);
    }
}
