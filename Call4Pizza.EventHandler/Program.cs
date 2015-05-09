using Call4Pizza.Models;
using Call4Pizza.Models.AggregateRoots;
using Call4Pizza.Models.Commands;
using Call4Pizza.Models.Contracts;
using Call4Pizza.Models.DTO;
using Call4Pizza.Models.Events;
using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.EventHandler
{
    public static class Program
    {
        private static IoCContainer Container = new IoCContainer();

        static void Main()
        {
            JobHost();
        }

        static void JobHost()
        {
            var config = new JobHostConfiguration();
            var host = new JobHost(config);
            host.RunAndBlock();
            host.Start();
        }

        public async static void OnOrderSendCommandPizza(
            [ServiceBusTrigger("events", "pizza")] EventEnvelope<OrderCreated> envelope,
            TextWriter log)
        {
            try
            {
                var repository = Container.Resolve<IRepository<QueryingOrder, Guid>>();
                var e = envelope.Event as OrderCreated;
                var order = repository.GetById(e.EventId);

                var pizzas = order.PizzasToDo();
                if (pizzas.Any())
                {
                    var append = Container.Resolve<IDataSourceAppend<PizzaToDoDTO>>();
                    await append.AppendAsync(pizzas);
                    var client = Container.Resolve<ITotem>();
                    await client.NewPizzasToPrepareAsync(e.EventId);
                }
            }
            catch (Exception ex)
            {
                // log...
            }
        }
    }
}
