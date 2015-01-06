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
        private IDialogService _dialogService;
        private ISettingRepository _settingRepo;
        public ImportViewModel(INavigationService navigator, IUploadRepository uploadRepo, IDialogService dialogService, ISettingRepository settingRepo)
        {
            _navigator = navigator;
            _uploadRepo = uploadRepo;
            _dialogService = dialogService;
            _settingRepo = settingRepo;
        }

        public void UploadCSV(StorageFile file)
        {
            _uploadRepo.ToDatabase(file);
            _dialogService.ShowMessage("We hebben " + _settingRepo.FindByKey("Income.CreditorName") + " als inkomen gevonden.", "Inkomen", "Ga naar categoriseren", () => _navigator.NavigateTo("Categorize"));
        }
    }
}
