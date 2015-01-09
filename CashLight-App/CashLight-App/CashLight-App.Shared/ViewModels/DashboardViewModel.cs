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
            IEnumerable<Period> allPeriods = _periodRepo.GetAll();

            Periods = new ObservableCollection<Period>();

            if (allPeriods.Count() > 0)
            {
                foreach (Period period in allPeriods)
                {
                    period.ImportantIncomes = SetTransactionHeight(period.ImportantIncomes);
                    period.ImportantSpendingCategories = SetCategoryHeight(period.ImportantSpendingCategories.ToList()).ToList();

                    Periods.Add(period);
                }
            }
            else
            {
                Periods.Add(_periodRepo.GetByDate(DateTime.Now));
                _dialogService.ShowError("U heeft nog geen CSV-bestand van uw bank geüpload.", "Geen transacties gevonden!", "App sluiten", () => Application.Current.Exit());
            }

            SelectedPeriod = Periods.Last();
        }

        private IEnumerable<Transaction> SetTransactionHeight(IEnumerable<Transaction> transactions)
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

        

        private List<ImportantCategory> SetCategoryHeight(List<ImportantCategory> categories)
        {
            if (categories.Count() > 0)
            {
                double highest = categories.Max(x => x.Category.Budget);

                double minHeight = 230;
                double maxHeight = 500;
                double marginTop = 70;
                if (Window.Current != null)
                {
                    maxHeight = (Window.Current.Bounds.Height / 2) - marginTop;
                }
                double useableHeight = maxHeight - minHeight;

                foreach (ImportantCategory item in categories)
                {
                    double percentage = (item.Category.Budget / highest);
                    item.Height = (useableHeight * percentage) + minHeight;
                }
            }

            return categories.ToList();
        }

        public MarkerHeightProperties getMarkerHeightProperties()
        {
            double minHeight = 230;
            double maxHeight = 500;
            double marginTop = 70;
            if (Window.Current != null)
            {
                maxHeight = (Window.Current.Bounds.Height / 2) - marginTop;
            }
            double useableHeight = maxHeight - minHeight;
            return new MarkerHeightProperties(minHeight, maxHeight, marginTop, useableHeight);
        }

        private void ShowTransactionDetails(Transaction transaction)
        {
            _dialogService.ShowMessage(transaction.Description, "Details van transactie");
        }
    }

    public class MarkerHeightProperties
    {
        public MarkerHeightProperties (double min, double max, double margin, double useableheight)
	{
            this.minHeight = min;
            this.maxHeight = max;
            this.marginTop = margin;
            this.useableHeight = useableHeight;
	}
        public double minHeight { get; set; }
        public double maxHeight { get; set; }
        public double marginTop { get; set; }
        public double useableHeight { get; set; }
    }
}
