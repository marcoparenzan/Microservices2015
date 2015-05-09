using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models
{
    public interface IEvent
    {
        TEvent As<TEvent>()
            where TEvent : IEvent;
        Guid EventId { get; }
        EventSource Source { get; }
    }
}
