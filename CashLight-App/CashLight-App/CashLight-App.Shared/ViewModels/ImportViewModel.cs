using CashLight_App.Models;
using CashLight_App.Models.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Storage;

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

        public void UploadCSV(StorageFile file)
        {
            IBank bank = new Models.ING();

            Upload upload = new Upload();
            upload.ToDatabase(bank, file);
        }
    }
}
