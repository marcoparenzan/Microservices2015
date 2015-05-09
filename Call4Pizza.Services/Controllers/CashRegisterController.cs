using Call4Pizza.Models.Commands;
using Call4Pizza.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Call4Pizza.Services.Controllers
{
    [Authorize]
    [RoutePrefix("CashRegister")]
    public class CashRegisterController : ApiController
    {
        private ICashRegister _cashRegister;
        protected ICashRegister CashRegister
        {
            get { return _cashRegister; }
        }

        public CashRegisterController(ICashRegister cashRegister)
        {
            _cashRegister = cashRegister;
        }

        [Route("CreateOrder")]
        [HttpPost]
        public async Task CreateOrder([FromBody] CreateOrder command)
        {
            await CashRegister.Handle(command);
        }

        [AllowAnonymous]
        [Route("Heartbeat")]
        [HttpGet]
        public DateTime Heartbeat()
        {
            return DateTime.Now;
        }
    }
}
