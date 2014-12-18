using CashLight_App.Tables;
using CashLight_App.Services.Interface;
using CashLight_App.Views.Categorize;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Windows.UI.Xaml.Controls;
using CashLight_App.Models;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System.Globalization;
using System.Diagnostics;
using GalaSoft.MvvmLight;

namespace CashLight_App.ViewModels
{
    public class CategorizeViewModel : ViewModelBase
    {
        private Transaction _transactionModel;
        private List<Transaction> _transactions;
        public Transaction _currentTransaction;
        private IUnitOfWork _unitOfWork;
        private INavigationService _navigation;

        public RelayCommand<string> ButtonCommand { get; set; }


        private string _name;
        public string Name
        {
            get {
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
        public string AfBij { 
            get
            {
                return _afbij;
            } 
            set
            {
                RaisePropertyChanged(() => AfBij);
            }
        }
        public CategorizeViewModel(IUnitOfWork unitOfWork, INavigationService NavigationService)
        {
            this._unitOfWork = unitOfWork;
            _transactionModel = new Transaction();
            _transactions = Transaction.All().Where(q => q.CategoryID == 0).ToList();
            _navigation = NavigationService;
            if (_transactions.Count <= 0)
            {
                _navigation.NavigateTo("Dashboard");
            }
            else
            {
                Name = _transactions.First().Naam;
                Amount = _transactions.First().Bedrag.ToString("C", new CultureInfo("nl-NL"));
                AfBij = _transactions.First().AfBij.ToString();

                //ButtonCommand = new RelayCommand<int>(ShowNextTransactionView);
                ButtonCommand = new RelayCommand<string>((param) => this.ShowNextTransactionView(param));
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


                _currentTransaction = nextTransaction;
                Name = _currentTransaction.Naam;
                Amount = _currentTransaction.Bedrag.ToString("C", new CultureInfo("nl-NL"));
                AfBij = _currentTransaction.AfBij.ToString();
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
