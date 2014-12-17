using CashLight_App.Tables;
using CashLight_App.Models;
using CashLight_App.Models.Interfaces;
using CashLight_App.Services.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;

namespace CashLight_App.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public RelayCommand RandomCategoriesCommand { get; set; }

        private IPeriodModel _selectedPeriod;
        public IPeriodModel SelectedPeriod
        {
            get
            {
                return _selectedPeriod;
            }
            set
            {
                if (_selectedPeriod != null)
                {
                    if (value.EndDate < _selectedPeriod.StartDate)
                    {
                        Periods.Add(value.Previous());
                    }
                }

                _selectedPeriod = value;
                RaisePropertyChanged(() => SelectedPeriod);
            }
        }

        public ObservableCollection<IPeriodModel> Periods { get; set; }

        public DashboardViewModel()
        {
            RandomCategoriesCommand = new RelayCommand(() => Category.SetRandomCategories());

            Periods = new ObservableCollection<IPeriodModel>();
            InitPeriods();
        }

        private void InitPeriods()
        {
            IPeriodModel now = new Period(DateTime.Now, false);

            Periods.Add(now);
            Periods.Add(now.Previous());

            SelectedPeriod = now;
        }
    }
}
