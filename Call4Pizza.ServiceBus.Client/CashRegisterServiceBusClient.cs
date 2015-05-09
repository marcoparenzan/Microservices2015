using Call4Pizza.Models;
using Call4Pizza.Models.Commands;
using Call4Pizza.Models.Contracts;
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
    public class CashRegisterServiceBusClient: ICashRegister
    {
        public CashRegisterServiceBusClient()
        {
        }

        async Task<ICashRegister> ICashRegister.Handle(CreateOrder command)
        {
            var message = new BrokeredMessage(new CommandEnvelope<CreateOrder>
            {
                Timestamp = DateTime.UtcNow
                ,
                Command = command
                ,
                Username = "<test>"
            });

            // Create the queue if it does not exist already
            string connectionString =
                CloudConfigurationManager.GetSetting("CashRegisterCommandsServiceBus");

            var client =
                    QueueClient.CreateFromConnectionString(connectionString, "Commands");
            await client.SendAsync(message);
            return this;
        }
    }
}
