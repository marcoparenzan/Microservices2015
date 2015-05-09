using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Entities
{
    public class OrderPizza: Call4PizzaEntity<string>
    {
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
