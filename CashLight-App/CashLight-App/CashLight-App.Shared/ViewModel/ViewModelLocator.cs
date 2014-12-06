using Microsoft.Practices.ServiceLocation;

namespace CashLight_App.ViewModel
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
