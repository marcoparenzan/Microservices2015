using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Models.Commands
{
    public class CreateOrder : Call4PizzaCommand
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }

        public int PizzaMargherita { get; set; }
        public int PizzaCapricciosa { get; set; }
        public int PizzaDiavola { get; set; }
        public int CocaCola { get; set; }
        public int Beer { get; set; }
    }
}
