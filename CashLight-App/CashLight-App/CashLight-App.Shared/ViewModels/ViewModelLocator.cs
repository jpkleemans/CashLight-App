using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CashLight_App.Config;
using GalaSoft.MvvmLight;
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

        public HeaderViewModel Header
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HeaderViewModel>();
            }
        }

        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                ContainerBuilder container = new ContainerBuilder();
                container.RegisterModule(new DesigntimeModule());
                ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(container.Build()));
            }
        }
    }
}
