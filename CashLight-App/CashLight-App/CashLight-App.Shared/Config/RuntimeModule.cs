using Autofac;
using CashLight_App.ViewModel;
using GalaSoft.MvvmLight.Views;

namespace CashLight_App.Config
{
    class RuntimeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterInstance<INavigationService>(Routes.GetRoutes(new NavigationService()));

            // ViewModels
            builder.RegisterType<DashboardViewModel>();
        }
    }
}
