using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace CashLight_App.ViewModels
{
    public class MenuViewModel : ViewModelBase
    {

        private INavigationService _navigationService;

        public MenuViewModel(INavigationService Navigation)
        {
            _navigationService = Navigation;
        }

        public void ExitApplication()
        {
            Application.Current.Exit();
        }

        public void NavigateToCategorize()
        {
            _navigationService.NavigateTo("Categorize");
        }
    }
}
