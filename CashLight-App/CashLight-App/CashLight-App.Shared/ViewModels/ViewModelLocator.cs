using Microsoft.Practices.ServiceLocation;

namespace CashLight_App.ViewModels
{
    public class ViewModelLocator
    {
        public DashboardViewModel Dashboard
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DashboardViewModel>();
            }
        }
        public ImportViewModel Import
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ImportViewModel>();
            }
        }

        public MenuViewModel Menu 
        { 
            get
            {
                return ServiceLocator.Current.GetInstance<MenuViewModel>();
            }
        }
        public CategorizeViewModel Categorize
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategorizeViewModel>();
            }
        }

        
    }
}
