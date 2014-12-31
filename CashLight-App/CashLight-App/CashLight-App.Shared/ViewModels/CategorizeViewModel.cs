using CashLight_App.Models;
using CashLight_App.Services.Interfaces;
using CashLight_App.Views.Categorize;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Globalization;
using System.Diagnostics;
using GalaSoft.MvvmLight;
using CashLight_App.Repositories.Interfaces;

namespace CashLight_App.ViewModels
{
    public class CategorizeViewModel : ViewModelBase
    {
        private Transaction _transactionModel;
        private List<Transaction> _transactions;
        public Transaction _currentTransaction;
        private INavigationService _navigation;

        public RelayCommand<string> ButtonCommand { get; set; }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private string _amount;
        public string Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                RaisePropertyChanged(() => Amount);
            }
        }

        private string _afbij;
        private ITransactionRepository _transactionRepository;
        public string AfBij
        {
            get
            {
                return _afbij;
            }
            set
            {
                _afbij = value;
                RaisePropertyChanged(() => AfBij);
            }
        }
        public CategorizeViewModel(INavigationService NavigationService, ITransactionRepository transactionRepository)
        {
            _transactionModel = new Transaction();
            _navigation = NavigationService;
            _transactionRepository = transactionRepository;

            _transactions = _transactionRepository.FindAll().Where(q => q.CategoryID == 0).ToList();

            if (_transactions.Count <= 0)
            {
                _navigation.NavigateTo("Dashboard");
            }
            else
            {
                _currentTransaction = _transactions.First();
                Name = _transactions.First().CreditorName;
                Amount = _transactions.First().Amount.ToString("C", new CultureInfo("nl-NL"));
                if (_currentTransaction.InOut == 0)
                {
                    AfBij = "Af";
                }
                else
                {
                    AfBij = "Bij";
                }

                //ButtonCommand = new RelayCommand<int>(ShowNextTransactionView);
                ButtonCommand = new RelayCommand<string>((param) => this.ShowNextTransactionView(param));
                _transactions.Remove(_currentTransaction);
            }
        }

        /// <summary>
        /// Hides the current transaction and shows the next transaction
        /// </summary>
        public void ShowNextTransactionView(string s)
        {
            int i = Convert.ToInt32(s);
            if (_transactions.Count > 0)
            {
                Transaction nextTransaction = _transactions.First(); // Get the next transaction from the list

                SaveCategory(i, _currentTransaction);

                Name = _currentTransaction.CreditorName;
                Amount = _currentTransaction.Amount.ToString("C", new CultureInfo("nl-NL"));
                if (_currentTransaction.InOut == 0)
                {
                    AfBij = "Af";
                }
                else
                {
                    AfBij = "Bij";
                }

                _currentTransaction = nextTransaction;
                _transactions.Remove(nextTransaction); // Remove transaction from list, so the next transaction becomes the first of the list

            }
            else
            {
                _navigation.NavigateTo("Dashboard");
            }
        }


        public void SaveCategory(int category, Transaction trans)
        {
            //    Category c = Kernel.Database.Category.Find(q => q.Naam == category.ToString()).FirstOrDefault();
            //    if (c != null)
            //    {
            //        _currentTransaction.CategoryID = c.CategoryID;
            //        Kernel.Database.Commit();
            //    }
            //    Model.Transaction.removeEqualTransactions(ref _transactions, _currentTransaction);
        }
    }
}
