using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Commands
{
    public interface ICashRegister
    {
        Task<ICashRegister> PlaceOrder(PlaceOrderCommand command);
    }
}
