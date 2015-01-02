using Autofac;
using Autofac.Extras.CommonServiceLocator;
using CashLight_App.Config;
using CashLight_App.Enums;
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

        /// <summary>
        /// Only for designtime
        /// Comment this if Visual Studio crashes in xaml
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                Bootstrapper.Initialize(Mode.Runtime);
            }
        }
    }
}
