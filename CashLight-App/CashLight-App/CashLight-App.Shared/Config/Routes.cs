using CashLight_App.Views.Categorize;
using CashLight_App.Views.Dashboard;
using GalaSoft.MvvmLight.Views;
using CashLight_App.Views.Category;

namespace CashLight_App.Config
{
    class Routes
    {
        public static NavigationService GetRoutes(NavigationService nav)
        {
            nav.Configure("Dashboard", typeof(DashboardView));
            nav.Configure("AddCategory", typeof(AddCategoryView));
            nav.Configure("Categorize", typeof(CategorizeView));

            return nav;
        }
    }
}
