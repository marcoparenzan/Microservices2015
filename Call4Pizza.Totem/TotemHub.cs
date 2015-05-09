using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Call4Pizza.Totem
{
    public class TotemHub : Hub
    {
        public static IHubContext Default
        {
            get
            {
                return
                    Microsoft.AspNet.SignalR
                    .GlobalHost
                    .ConnectionManager
                    .GetHubContext<TotemHub>();
            }
        }
    }
}