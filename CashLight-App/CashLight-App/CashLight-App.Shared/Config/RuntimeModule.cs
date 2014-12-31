using Autofac;
using CashLight_App.Repositories;
using CashLight_App.Repositories.Interfaces;
using CashLight_App.Services;
using CashLight_App.Services.Interfaces;
using CashLight_App.Models;
using CashLight_App.ViewModels;
using GalaSoft.MvvmLight.Views;
using CashLight_App.Models.Interface;

namespace CashLight_App.Config
{
    class RuntimeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterInstance<INavigationService>(Routes.GetRoutes(new NavigationService()))
                .SingleInstance();

            builder.RegisterType<Database>()
               .As<IDatabase>()
               .WithParameter("name", "CashLight.db")
               .SingleInstance();

            // ViewModels
            builder.RegisterType<DashboardViewModel>();
            builder.RegisterType<ImportViewModel>();
            builder.RegisterType<MenuViewModel>();
            builder.RegisterType<CategorizeViewModel>();
            builder.RegisterType<HeaderViewModel>();

            // Repositories
            builder.RegisterType<PeriodRepository>()
               .As<IPeriodRepository>()
               .SingleInstance();

            builder.RegisterType<TransactionRepository>()
               .As<ITransactionRepository>()
               .SingleInstance();

            builder.RegisterType<UploadRepository>()
              .As<IUploadRepository>()
              .SingleInstance();
            
        }
    }
}
