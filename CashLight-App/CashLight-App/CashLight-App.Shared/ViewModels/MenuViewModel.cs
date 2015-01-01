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
        private INavigationService _navigationservice;

        public RelayCommand GoToCategorizeCommand { get; set; }
        public RelayCommand GoToDashboardCommand { get; set; }
        public RelayCommand ExitApplicationCommand { get; set; }

        public MenuViewModel(INavigationService Navigation)
        {
            _navigationservice = Navigation;

            GoToCategorizeCommand = new RelayCommand(NavigateToCategorize);
            GoToDashboardCommand = new RelayCommand(NavigateToDashboard);
            ExitApplicationCommand = new RelayCommand(ExitApplication);
        }

        public void ExitApplication()
        {
            Application.Current.Exit();
        }

        public void NavigateToCategorize()
        {
            _navigationservice.NavigateTo("Categorize");
        }

        public void NavigateToDashboard()
        {
            _navigationservice.NavigateTo("Dashboard");
        }

    }
}
