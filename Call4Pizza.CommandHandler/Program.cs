using Call4Pizza.Models;
using Call4Pizza.Models.AggregateRoots;
using Call4Pizza.Models.Commands;
using Call4Pizza.Models.Contracts;
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

namespace Call4Pizza.CommandHandler
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

        public static void OnCreateOrderCommand(
            [ServiceBusTrigger("commands")] CommandEnvelope<CreateOrder> envelope,
            TextWriter log)
        {
            var command = envelope.Command as CreateOrder;
            var repository = Container.Resolve<IRepository<CommandingOrder, Guid>>();
            var client = Container.Resolve<ICommandHandler>();

            var order = repository.GetById(command.CommandId);
            var events = new List<EventEnvelope<OrderCreated>>();
            order.Event += (s, e) =>
            {
                if (e is OrderCreated)
                {
                    events.Add(new EventEnvelope<OrderCreated>
                    {
                        Event = (OrderCreated)e
                        ,
                        Timestamp = DateTime.UtcNow
                        ,
                        Username = envelope.Username
                    });
                }
            };
            order.Apply(command);
            repository.Set(order);
            client.Handle(events.ToArray());
        }
    }
}
