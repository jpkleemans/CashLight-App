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

            container.RegisterModule(new RuntimeModule());

            IComponentContext build = container.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(build));
        }
    }
}
