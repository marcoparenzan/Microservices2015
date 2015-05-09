using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Events
{
    public abstract class Call4PizzaEvent: IEvent
    {
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public EventSource Source { get; set; }

        TEvent IEvent.As<TEvent>()
        {
            if (this is TEvent)
            {
                return (TEvent)(IEvent)this;
            }
            throw new InvalidCastException("This is not a " + typeof(TEvent).Name);
        }
    }
}
