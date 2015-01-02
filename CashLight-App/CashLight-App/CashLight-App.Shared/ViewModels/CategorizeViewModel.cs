using CashLight_App.Models;
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
using System.Collections.ObjectModel;

namespace CashLight_App.ViewModels
{
    public class CategorizeViewModel : ViewModelBase
    {
        private ICategoryRepository _categoryRepo;
        private ITransactionRepository _transactionRepo;

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }

        private Transaction _currentTransaction;
        public Transaction CurrentTransaction
        {
            get
            {
                return _currentTransaction;
            }
            set
            {
                _currentTransaction = value;
                RaisePropertyChanged(() => CurrentTransaction);
            }
        }

        public CategorizeViewModel(ICategoryRepository categoryRepo, ITransactionRepository transactionRepo)
        {
            _categoryRepo = categoryRepo;
            _transactionRepo = transactionRepo;

            Categories = new ObservableCollection<Category>(_categoryRepo.FindAll());
            Transactions = new ObservableCollection<Transaction>(_transactionRepo.GetAllSpendings());

            CurrentTransaction = Transactions.First();
        }

        public void SetCategory(int categoryID)
        {
            Transactions.Remove(CurrentTransaction);

            CurrentTransaction = Transactions.First();
        }
    }
}
