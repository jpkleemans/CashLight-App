using Autofac;
using CashLight_App.ViewModel;

namespace CashLight_App.Config
{
    class RuntimeModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DashboardViewModel>();
        }
    }
}
