using Call4Pizza.Services.Controllers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Call4Pizza.Services
{
    public class IoCContainer : WindsorContainer, IHttpControllerActivator 
    {
        public IoCContainer()
        {
            Register(Classes.FromAssemblyNamed("Call4Pizza.Models").InNamespace("Call4Pizza.Models.Contracts"));
            Register(Classes.FromAssemblyNamed("Call4Pizza.Services").InNamespace("Call4Pizza.Services.Controllers").LifestylePerWebRequest());
            Register(Classes.FromAssemblyNamed("Call4Pizza.ServiceBus.Client").InNamespace("Call4Pizza.ServiceBus.Client").WithServiceAllInterfaces());
        }

        IHttpController IHttpControllerActivator.Create(System.Net.Http.HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, System.Type controllerType)
        {
            switch (controllerDescriptor.ControllerName)
            {
                case "CashRegister":
                    var xxx = Resolve<CashRegisterController>();;
                    return xxx;
                default:
                    return null;
            }
        }
    }
}
