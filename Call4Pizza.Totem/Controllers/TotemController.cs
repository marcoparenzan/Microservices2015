using Call4Pizza.Models;
using Call4Pizza.Models.Commands;
using Call4Pizza.Models.Contracts;
using Call4Pizza.Models.DTO;
using Call4Pizza.Storage.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Call4Pizza.Totem.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("Totem")]
    public class TotemController : ApiController
    {
        public TotemController()
        {
        }

        [Route("NewPizzasToPrepare")]
        [HttpGet]
        public void NewPizzasToPrepare(string commandId)
        {
            TotemHub.Default.Clients.All.newPizzasToPrepare(commandId);
        }

        [Route("NewBeveragesToPrepare")]
        [HttpPost]
        public void NewBeveragesToPrepare(string commandId)
        {
            TotemHub.Default.Clients.All.NewBeveragesToPrepare(commandId);
        }

        [Route("PizzasToDo")]
        [HttpGet]
        public IEnumerable<PizzaToDoDTO> PizzasToDo(Guid? commandId)
        {
            var dataSource = new PizzasToDoDataSource();
            return dataSource.GetByCommandId(commandId.Value);
        }

        [Route("HeartBeat")]
        [HttpGet]
        public DateTime HeartBeat()
        {
            return DateTime.Now;
        }
    }
}
