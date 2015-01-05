using CashLight_App.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using CashLight_App.Repositories.Interfaces;
using System.Diagnostics;
using GalaSoft.MvvmLight.Views;
using System.Globalization;

namespace CashLight_App.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private IPeriodRepository _periodRepo;
        private IDialogService _dialogService;

        public string Title
        {
            get
            {
                return String.Format("{0} t/m {1}", SelectedPeriod.StartDate.ToString("dd-MM-yyyy"), SelectedPeriod.EndDate.ToString("dd-MM-yyyy"));
            }
        }

        public string SpendingsLimit
        {
            get
            {
                return String.Format(new CultureInfo("nl-NL"), "Bestedingsruimte: {0:c}", SelectedPeriod.SpendingsLimit);
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
                //if (_selectedPeriod != null)
                //{
                //    if (value.EndDate < _selectedPeriod.StartDate)
                //    {
                //        Period previous = _periodRepo.GetByDate(value.StartDate.AddDays(-1));

                //        previous.ImportantIncomes = SetHeight(previous.ImportantIncomes);
                //        previous.ImportantSpendings = SetHeight(previous.ImportantSpendings);

                //        Periods.Add(previous);
                //    }
                //}

                _selectedPeriod = value;
                RaisePropertyChanged(() => SelectedPeriod);
                RaisePropertyChanged(() => Title);
                RaisePropertyChanged(() => SpendingsLimit);
            }
        }

        public ObservableCollection<Period> Periods { get; set; }

        public RelayCommand<Transaction> ShowTransactionDetailsCommand { get; set; }

        public DashboardViewModel(IPeriodRepository periodRepo, IDialogService dialogService)
        {
            _periodRepo = periodRepo;
            _dialogService = dialogService;

            ShowTransactionDetailsCommand = new RelayCommand<Transaction>((transaction) => ShowTransactionDetails(transaction));

            InitPeriods();
        }

        private void InitPeriods()
        {
            //Periods = new ObservableCollection<Period>();

            //Period now = _periodRepo.GetByDate(DateTime.Now);
            //Period previous = _periodRepo.GetByDate(now.StartDate.AddDays(-1));

            //now.ImportantIncomes = SetHeight(now.ImportantIncomes);
            //now.ImportantSpendings = SetHeight(now.ImportantSpendings);
            //previous.ImportantIncomes = SetHeight(previous.ImportantIncomes);
            //previous.ImportantSpendings = SetHeight(previous.ImportantSpendings);

            //Periods.Add(now);
            //Periods.Add(previous);

            //SelectedPeriod = now;

            Periods = new ObservableCollection<Period>();

            IEnumerable<Period> allPeriods = _periodRepo.GetAll();

            foreach (Period period in allPeriods)
            {
                period.ImportantIncomes = SetHeight(period.ImportantIncomes);
                period.ImportantSpendings = SetHeight(period.ImportantSpendings);

                Periods.Add(period);
            }

            SelectedPeriod = Periods.Last();
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

        private void ShowTransactionDetails(Transaction transaction)
        {
            _dialogService.ShowMessage(transaction.Description, "Details van transactie");
        }
    }
}
