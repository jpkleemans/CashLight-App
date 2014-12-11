using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashLight_App.ViewModels
{
    public class ImportViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        public RelayCommand GoToDashboardCommand { get; set; }

        public ImportViewModel(INavigationService navigationService)
        {
            GoToDashboardCommand = new RelayCommand(GoToDashboard);

            _navigationService = navigationService;
        }

        private void GoToDashboard()
        {
            _navigationService.NavigateTo("Dashboard");
        }
    }
}
