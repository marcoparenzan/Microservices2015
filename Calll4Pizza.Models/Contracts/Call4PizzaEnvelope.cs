using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Contracts
{
    public abstract class Call4PizzaEnvelope: IEnvelope
    {
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
