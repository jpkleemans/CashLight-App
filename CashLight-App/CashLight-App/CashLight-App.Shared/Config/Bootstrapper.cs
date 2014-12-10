using Autofac;
using Autofac.Extras.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;

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

            Debug.WriteLine(Windows.Storage.ApplicationData.Current.LocalFolder.Path);
        }
    }
}
