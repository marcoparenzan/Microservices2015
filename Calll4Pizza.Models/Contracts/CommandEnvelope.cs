using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Contracts
{
    public class CommandEnvelope<TCommand> : Call4PizzaEnvelope
        where TCommand: ICommand
    {
        public TCommand Command { get; set; }
    }
}
