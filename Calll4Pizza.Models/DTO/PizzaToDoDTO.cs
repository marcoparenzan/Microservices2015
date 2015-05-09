using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.DTO
{
    public class PizzaToDoDTO: IDTO
    {
        public Guid CommandId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
    }
}
