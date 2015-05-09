using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Contracts
{
    public class EventEnvelope<TEvent> : Call4PizzaEnvelope
        where TEvent : IEvent
    {
        public TEvent Event { get; set; }
    }
}
