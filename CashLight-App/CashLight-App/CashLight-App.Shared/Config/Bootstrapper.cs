using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CashLight_App.Enums;
using Microsoft.Practices.ServiceLocation;
using System.Diagnostics;

namespace CashLight_App.Config
{
    public class Bootstrapper
    {
        public static void Initialize(Mode mode)
        {
            Debug.WriteLine(Windows.Storage.ApplicationData.Current.LocalFolder.Path.ToString());

            ContainerBuilder container = new ContainerBuilder();

            switch (mode)
            {
                case Mode.Runtime:
                    container.RegisterModule(new RuntimeModule());
                    break;
                case Mode.Testing:
                    container.RegisterModule(new TesttimeModule());
                    break;
            }

            IComponentContext build = container.Build();

            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(build));
        }
    }
}
