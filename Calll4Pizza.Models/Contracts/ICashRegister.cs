using Call4Pizza.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Contracts
{
    public interface ICashRegister
    {
        Task<ICashRegister> Handle(CreateOrder command);
    }
}
