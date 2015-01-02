using CashLight_App.Repositories.Interfaces;
using CashLight_App.Models;
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
        private INavigationService _navigator;
        private IUploadRepository _uploadRepo;
        public RelayCommand GoToDashboardCommand { get; set; }

        public ImportViewModel(INavigationService navigator, IUploadRepository uploadRepo)
        {
            GoToDashboardCommand = new RelayCommand(GoToDashboard);

            _navigator = navigator;
            _uploadRepo = uploadRepo;
        }

        private void GoToDashboard()
        {
            _navigator.NavigateTo("Dashboard");
        }

        public void UploadCSV(StorageFile file)
        {
            _uploadRepo.ToDatabase(file);
        }
    }
}
