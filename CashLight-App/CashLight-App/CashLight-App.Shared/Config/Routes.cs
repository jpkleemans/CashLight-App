using CashLight_App.Views.Categorize;
using CashLight_App.Views.Dashboard;
using GalaSoft.MvvmLight.Views;

namespace CashLight_App.Config
{
    class Routes
    {
        public static NavigationService GetRoutes(NavigationService nav)
        {
            nav.Configure("Dashboard", typeof(DashboardView));
            nav.Configure("Categorize", typeof(CategorizeView));

            return nav;
        }
    }
}
