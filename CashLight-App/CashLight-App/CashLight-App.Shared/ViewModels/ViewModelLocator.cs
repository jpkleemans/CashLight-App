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
    }
}
