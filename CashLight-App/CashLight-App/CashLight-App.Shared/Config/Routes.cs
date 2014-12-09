using CashLight_App.Views.Dashboard;
using GalaSoft.MvvmLight.Views;

namespace CashLight_App.Config
{
    class Routes
    {
        public static NavigationService GetRoutes(NavigationService nav)
        {
            nav.Configure("Dashboard", typeof(DashboardView));

            return nav;
        }
    }
}
