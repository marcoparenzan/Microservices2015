using Call4Pizza.Models;
using Call4Pizza.Models.Commands;
using Call4Pizza.Models.Contracts;
using Call4Pizza.Models.Events;
using Microsoft.Azure;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call4Pizza.ServiceBus.Client
{
    public class CommandHandlerServiceBusClient: ICommandHandler
    {
        public CommandHandlerServiceBusClient()
        {
        }

        async private Task<ICommandHandler> Handle(OrderCreated e)
        {
            var envelope = new EventEnvelope<OrderCreated>
            {
                Timestamp = DateTime.UtcNow
                ,
                Event = e
                ,
                Username = "<test>"
            };
            return await ((ICommandHandler)this).Handle(envelope);
        }

        private async Task<ICommandHandler> Handle(EventEnvelope<OrderCreated> e)
        {
            // Create the queue if it does not exist already
            string connectionString =
                CloudConfigurationManager.GetSetting("EventsServiceBus");

            try
            {
                var client =
                        TopicClient.CreateFromConnectionString(connectionString, "Events");
                await client.SendAsync(new BrokeredMessage(e));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return this;
        }

        async Task<ICommandHandler> ICommandHandler.Handle(params EventEnvelope<OrderCreated>[] ee)
        {
            foreach (var e in ee)
            {
                await Handle(e); 
            }
            return this;
        }
    }
}
