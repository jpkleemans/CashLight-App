using Autofac;
using CashLight_App.Repositories;
using CashLight_App.Repositories.Interfaces;
using CashLight_App.Services;
using CashLight_App.Services.BankConverter;
using CashLight_App.Services.CSVReader;
using CashLight_App.Services.SQLite;
using CashLight_App.ViewModels;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Config
{
    class TesttimeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Services
            builder.RegisterInstance<INavigationService>(Routes.GetRoutes(new NavigationService()))
                .SingleInstance();

            builder.RegisterType<DialogService>()
              .As<IDialogService>()
              .SingleInstance();

            builder.RegisterType<SQLiteService>()
               .As<ISQLiteService>()
               .WithParameter("name", "CashLightTest.db")
               .SingleInstance();

            builder.RegisterType<CSVReaderService>()
               .As<ICSVReaderService>()
               .SingleInstance();

            builder.RegisterType<BankConverterService>()
               .As<IBankConverterService>()
               .SingleInstance();

            // ViewModels
            builder.RegisterType<DashboardViewModel>();
            builder.RegisterType<CategoryViewModel>();
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

            builder.RegisterType<CategoryRepository>()
             .As<ICategoryRepository>()
             .SingleInstance();

            builder.RegisterType<UploadRepository>()
              .As<IUploadRepository>()
              .SingleInstance();

            builder.RegisterType<SettingRepository>()
            .As<ISettingRepository>()
            .SingleInstance();

            builder.RegisterType<AccountRepository>()
            .As<IAccountRepository>()
            .SingleInstance();
        }
    }
}
