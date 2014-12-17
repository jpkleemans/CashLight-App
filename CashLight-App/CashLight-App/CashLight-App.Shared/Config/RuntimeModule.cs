using Autofac;
using CashLight_App.Models;
using CashLight_App.Models.Interfaces;
using CashLight_App.Services;
using CashLight_App.Services.Interface;
using CashLight_App.ViewModels;
using GalaSoft.MvvmLight.Views;

namespace CashLight_App.Config
{
    class RuntimeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterInstance<INavigationService>(Routes.GetRoutes(new NavigationService()))
                .SingleInstance();

            builder.RegisterType<SQLiteUnitOfWork>()
               .As<IUnitOfWork>()
               .WithParameter("_dbname", "CashLight.db")
               .SingleInstance();

            // Models
            builder.RegisterType<PeriodModel>().As<IPeriodModel>();

            // ViewModels
            builder.RegisterType<DashboardViewModel>();
            builder.RegisterType<ImportViewModel>();
            builder.RegisterType<MenuViewModel>();

        }
    }
}
