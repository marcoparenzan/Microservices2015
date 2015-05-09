using Call4Pizza.Models;
using Call4Pizza.Models.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.SQL.Client
{
    public class PizzasToDoDataSource : DTODataSource<PizzaToDoDTO>, IDataSourceAppend<PizzaToDoDTO>
    {
        public PizzasToDoDataSource()
        {
            Pizzas = Set<TE>();
        }

        private DbSet<TE> Pizzas { get; set; }
        
        [Table("Pizzas")]
        class TE
        {
            public TE(PizzaToDoDTO dto)
            {
                this.CommandId = dto.CommandId;
                this.Date = dto.Date;
                this.Description = dto.Description;
                this.Quantity = dto.Quantity;
                this.Name = dto.Name;
                this.City = dto.City;
            }

            public Guid CommandId { get; set; }
            public DateTime Date { get; set; }
            public string Description { get; set; }
            public int Quantity { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
        }

        public async Task AppendAsync(IEnumerable<PizzaToDoDTO> dtos) 
        {
            foreach (var dto in dtos)
            {
                Pizzas.Add(new TE(dto));
            }
        }

        protected override IEnumerable<PizzaToDoDTO> OnGetByCommandId(Guid commandId)
        {
            var query = Pizzas;
            var items = query.Where(xx => xx.CommandId == commandId).Select(xx => new PizzaToDoDTO {
                CommandId = xx.CommandId
                ,
                Description = xx.Description
                ,
                Quantity = xx.Quantity
                ,
                City = xx.City
                ,
                Date = xx.Date
                ,
                Name = xx.Name
            }).ToList();
            return items;
        }
    }
}
