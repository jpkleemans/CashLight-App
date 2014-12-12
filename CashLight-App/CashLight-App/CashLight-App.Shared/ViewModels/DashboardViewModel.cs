using CashLight_App.DataModels;
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
                    /// PROBLEEM:
                    /// Hij pakt al de volgende index voordat hij gaat swipen,
                    /// Hierdoor zie je heel kort al de volgende index
                    /// en gaat de swipe ook de verkeerde kant op...

                    if (value.StartDate > _selectedPeriod.EndDate)
                    {
                        Periods[0] = _selectedPeriod;
                        Periods[1] = value;
                        Periods[2] = value.Next();
                    }
                    else if (value.EndDate < _selectedPeriod.StartDate)
                    {
                        Periods[0] = value.Previous();
                        Periods[1] = value;
                        Periods[2] = _selectedPeriod;
                    }
                }

                _selectedPeriod = value;
                RaisePropertyChanged(() => SelectedPeriod);
            }
        }

        public ObservableCollection<IPeriodModel> Periods { get; set; }

        public DashboardViewModel()
        {
            Periods = new ObservableCollection<IPeriodModel>();

            InitPeriods();
        }

        private void InitPeriods()
        {
            IPeriodModel now = new PeriodModel(DateTime.Now, false);

            Periods.Add(now.Previous());
            Periods.Add(now);
            Periods.Add(now.Next());

            SelectedPeriod = now;
        }
    }
}
