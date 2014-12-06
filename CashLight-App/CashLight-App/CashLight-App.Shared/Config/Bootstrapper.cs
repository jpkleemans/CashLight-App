using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;

namespace CashLight_App.Config
{
    class Bootstrapper
    {
        public Bootstrapper()
        {
            ContainerBuilder container = new ContainerBuilder();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container.Build()));

            container.RegisterModule(new RuntimeModule());
        }
    }
}
