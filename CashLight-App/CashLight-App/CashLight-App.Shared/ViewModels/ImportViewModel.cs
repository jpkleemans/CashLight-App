using CashLight_App.Repositories.Interfaces;
using CashLight_App.Models;
using CashLight_App.Services.Banks;
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
        private IUploadRepository _uploadRepository;
        public RelayCommand GoToDashboardCommand { get; set; }

        public ImportViewModel(INavigationService navigationService, IUploadRepository uploadRepository)
        {
            GoToDashboardCommand = new RelayCommand(GoToDashboard);

            _navigationService = navigationService;
            _uploadRepository = uploadRepository;
        }

        private void GoToDashboard()
        {
            _navigationService.NavigateTo("Dashboard");
        }

        public void UploadCSV(StorageFile file)
        {
            IBank bank = new ING();

            _uploadRepository.ToDatabase(bank, file);
        }
    }
}
