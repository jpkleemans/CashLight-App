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
        public ImportViewModel(INavigationService navigator, IUploadRepository uploadRepo)
        {
            _navigator = navigator;
            _uploadRepo = uploadRepo;
        }

        public void UploadCSV(StorageFile file)
        {
            _uploadRepo.ToDatabase(file);
            _navigator.NavigateTo("Categorize");
        }
    }
}
