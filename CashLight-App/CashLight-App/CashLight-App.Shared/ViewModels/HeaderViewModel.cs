﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.ViewModels
{
    public class HeaderViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public RelayCommand BackButtonCommand { get; set; }

        public HeaderViewModel(INavigationService Navigation)
        {
            _navigationService = Navigation;
            BackButtonCommand = new RelayCommand(GoToPreviousView);
        }

        public void GoToPreviousView()
        {
            _navigationService.GoBack();
        }
    }
}