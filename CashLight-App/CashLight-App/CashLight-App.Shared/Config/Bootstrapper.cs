using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CashLight_App.Models;
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

            CategoryModel.SetRandomCategories();
            Debug.WriteLine(Windows.Storage.ApplicationData.Current.LocalFolder.Path.ToString());

            
        }
    }
}
