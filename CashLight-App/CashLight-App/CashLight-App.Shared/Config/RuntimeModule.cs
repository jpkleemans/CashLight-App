using Autofac;
using CashLight_App.Interface;
using CashLight_App.Service;
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

            builder.RegisterType<SQLiteUnitOfWork>()
               .As<IUnitOfWork>()
               .WithParameter("_dbname", "CashLight.db")
               .SingleInstance();

            // ViewModels
            builder.RegisterType<DashboardViewModel>();
        }
    }
}
