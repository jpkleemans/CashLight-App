using CashLight_App.Models;
using CashLight_App.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using CashLight_App.Repositories.Interfaces;
using System.Diagnostics;

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

        private Period _selectedPeriod;
        public Period SelectedPeriod
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
                        Period previous = _periodRepository.GetByDate(value.StartDate.AddDays(-1));

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

        public ObservableCollection<Period> Periods { get; set; }

        public DashboardViewModel(IPeriodRepository periodRepository)
        {
            _periodRepository = periodRepository;

            InitPeriods();
        }

        private void InitPeriods()
        {
            Periods = new ObservableCollection<Period>();

            Period now = _periodRepository.GetByDate(DateTime.Now);
            Period previous = _periodRepository.GetByDate(now.StartDate.AddDays(-1));

            now.ImportantIncomes = SetHeight(now.ImportantIncomes);
            now.ImportantSpendings = SetHeight(now.ImportantSpendings);
            previous.ImportantIncomes = SetHeight(previous.ImportantIncomes);
            previous.ImportantSpendings = SetHeight(previous.ImportantSpendings);

            Periods.Add(now);
            Periods.Add(previous);

            SelectedPeriod = now;
        }

        private IEnumerable<Transaction> SetHeight(IEnumerable<Transaction> transactions)
        {
            if (transactions.Count() > 0)
            {
                double highest = transactions.Max(x => x.Amount);

                double minHeight = 230;
                double maxHeight = 500;
                double marginTop = 70;
                if (Window.Current != null)
                {
                    maxHeight = (Window.Current.Bounds.Height / 2) - marginTop;
                }
                double useableHeight = maxHeight - minHeight;

                foreach (Transaction item in transactions)
                {
                    double percentage = (item.Amount / highest);
                    item.Height = (useableHeight * percentage) + minHeight;
                }
            }

            return transactions;
        }
    }
}
