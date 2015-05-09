using Call4Pizza.CashRegister.ViewModels;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Call4Pizza.CashRegister
{
    public class IoCContainer : WindsorContainer 
    {
        public IoCContainer()
        {
            Register(Classes.FromAssemblyNamed("Call4Pizza.Models").InNamespace("Call4Pizza.Models.Contracts"));
            Register(Classes.FromAssemblyNamed("Call4Pizza.Http.Client").InNamespace("Call4Pizza.Http.Client").WithServiceAllInterfaces());
            Register(Classes.FromThisAssembly().InSameNamespaceAs<CashRegisterViewModel>());
        }
    }
}
