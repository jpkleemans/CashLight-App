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

namespace CashLight_App.ViewModels
{
    public class CategorizeViewModel
    {
        private CategorizeView _view;
        private Transaction _transactionModel;
        private List<Transaction> _transactions;
        public Transaction _currentTransaction;
        private IUnitOfWork _unitOfWork;
        private INavigationService _navigation;

        public RelayCommand<int> ButtonCommand { get; set; }

        public CategorizeViewModel(CategorizeView categorizeView, IUnitOfWork unitOfWork, INavigationService NavigationService)
        {
            this._view = categorizeView;
            this._unitOfWork = unitOfWork;
            _transactionModel = new Transaction();
            _transactions = Transaction.All().Where(q => q.CategoryID != null).ToList();
            _navigation = NavigationService;

            ButtonCommand = new RelayCommand<int>((i) => ShowNextTransactionView(i));
        }

        /// <summary>
        /// Hides the current transaction and shows the next transaction
        /// </summary>
        public void ShowNextTransactionView(int i)
        {              
            if (_transactions.Count > 0)
            {
                Transaction nextTransaction = _transactions.First(); // Get the next transaction from the list
                
                SaveCategory();

                _currentTransaction = nextTransaction;                

                _transactions.Remove(nextTransaction); // Remove transaction from list, so the next transaction becomes the first of the list
            }
            else
            {
                _navigation.NavigateTo("Dashboard");
            }
        }


        public void SaveCategory()
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
