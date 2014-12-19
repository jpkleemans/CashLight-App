using CashLight_App.Models;
using CashLight_App.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using CashLight_App.Business.Interfaces;
using System.Diagnostics;
using CashLight_App.Models.Interface;

namespace CashLight_App.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private IPeriodRepository _periodRepository;

        public string Title
        {
            get
            {
                return String.Format("{0} t/m {1}", SelectedPeriod.StartDate.ToString("dd-MM-yyyy"), SelectedPeriod.EndDate.ToString("dd-MM-yyyy"));
            }
        }

        private IPeriod _selectedPeriod;
        public IPeriod SelectedPeriod
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
                        IPeriod previous = _periodRepository.GetByDate(value.StartDate.AddDays(-1));
                        previous.ImportantIncomes = SetHeight(previous.ImportantIncomes);
                        previous.ImportantSpendings = SetHeight(previous.ImportantSpendings);
                        Periods.Add(previous);
                    }
                }

                _selectedPeriod = value;
                RaisePropertyChanged(() => SelectedPeriod);
                RaisePropertyChanged(() => Title);
            }
        }

        public ObservableCollection<IPeriod> Periods { get; set; }

        public DashboardViewModel(IPeriodRepository periodRepository)
        {
            _periodRepository = periodRepository;

            InitPeriods();
        }

        private void InitPeriods()
        {
            Periods = new ObservableCollection<IPeriod>();

            IPeriod now = _periodRepository.GetByDate(DateTime.Now);
            IPeriod previous = _periodRepository.GetByDate(now.StartDate.AddDays(-1));

            now.ImportantIncomes = SetHeight(now.ImportantIncomes);
            now.ImportantSpendings = SetHeight(now.ImportantSpendings);
            previous.ImportantIncomes = SetHeight(previous.ImportantIncomes);
            previous.ImportantSpendings = SetHeight(previous.ImportantSpendings);

            Periods.Add(now);
            Periods.Add(previous);

            SelectedPeriod = now;
        }

        private List<ITransaction> SetHeight(List<ITransaction> transactions)
        {
            double highest = 0;
            foreach (var item in transactions)
            {
                if (item.Amount > highest)
                {
                    highest = item.Amount;
                }
            }

            double maxHeight = 1080;

            if (Window.Current != null)
            {
                maxHeight = (Window.Current.Bounds.Height / 2) - 50; //Max height off the markers.
            }

            double minHeight = 230; //Min height off the markers.
            double useableHeight = maxHeight - minHeight;

            foreach (ITransaction item in transactions)
            {
                double percentage = (item.Amount / highest);

                item.Height = (useableHeight * percentage) + minHeight;
            }

            return transactions;
        }
    }
}
