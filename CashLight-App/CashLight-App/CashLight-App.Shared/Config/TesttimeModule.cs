using Autofac;
using CashLight_App.Services;
using CashLight_App.Services.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Config
{
    class TesttimeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SQLiteService>()
               .As<ISQLiteService>()
               .WithParameter("name", "CashLightTest.db")
               .SingleInstance();
        }
    }
}
