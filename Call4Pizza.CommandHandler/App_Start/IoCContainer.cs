using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Call4Pizza.CommandHandler
{
    public class IoCContainer : WindsorContainer 
    {
        public IoCContainer()
        {
            Register(Classes.FromAssemblyNamed("Call4Pizza.Models").InNamespace("Call4Pizza.Models.Contracts"));
            Register(Classes.FromAssemblyNamed("Call4Pizza.ServiceBus.Client").InNamespace("Call4Pizza.ServiceBus.Client").WithServiceAllInterfaces());
            Register(Classes.FromAssemblyNamed("Call4Pizza.Storage.Client").InNamespace("Call4Pizza.Storage.Client").WithServiceAllInterfaces());
        }
    }
}
