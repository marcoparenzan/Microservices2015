using Call4Pizza.Models;
using Call4Pizza.Models.DTO;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.Storage.Client
{
    public class PizzasToDoDataSource : DTODataSource<PizzaToDoDTO>, IDataSourceAppend<PizzaToDoDTO>
    {
        class TE : TableEntity
        {
            public TE()
            {
            }

            public TE(PizzaToDoDTO dto)
            {
                this.PartitionKey = dto.City;
                this.RowKey = string.Format("{0}_{1}", dto.CommandId, dto.Description);
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
            var pizzasToDo = this.TableClient.GetTableReference("pizzasToDo");
            var batch = new TableBatchOperation();
            foreach (var dto in dtos)
            {
                var op = TableOperation.Insert(new TE(dto));
                batch.Add(op);
            }
            await pizzasToDo.ExecuteBatchAsync(batch);
        }

        protected override IEnumerable<PizzaToDoDTO> OnGetByCommandId(Guid commandId)
        {
            var pizzasToDo = this.TableClient.GetTableReference("pizzasToDo");
            var query = pizzasToDo.CreateQuery<TE>();
            var items = query.Where(xx => xx.CommandId == commandId).Select(xx => new PizzaToDoDTO {
                CommandId = xx.CommandId
                ,
                Description = xx.Description
                ,
                Quantity = xx.Quantity
                ,
                Name = xx.Name
                ,
                City = xx.City
            }).ToList();
            return items;
        }
    }
}
