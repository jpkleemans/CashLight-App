using Autofac;
using CashLight_App.Services;
using CashLight_App.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Config
{
    class TesttimeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Database>()
               .As<IDatabase>()
               .WithParameter("name", "CashLightTest.db")
               .SingleInstance();
        }
    }
}
