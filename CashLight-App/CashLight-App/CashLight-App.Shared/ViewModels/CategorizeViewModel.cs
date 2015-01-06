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
using CashLight_App.Enums;

namespace CashLight_App.ViewModels
{
    public class CategorizeViewModel : ViewModelBase
    {
        private ICategoryRepository _categoryRepo;
        private ITransactionRepository _transactionRepo;
        private INavigationService _navigator;
        private IDialogService _dialogService;

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }

        public RelayCommand<int> SetCategoryCommand { get; set; }
        public RelayCommand AddCategoryCommand { get; set; }

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

        private string _remaining;
        public string Remaining
        {
            get
            {
                return "Nog " + _remaining + " transacties te gaan.";
            }
            set
            {
                _remaining = value;
                RaisePropertyChanged(() => Remaining);
            }
        }

        public CategorizeViewModel(INavigationService navigator,
                                   ICategoryRepository categoryRepo,
                                   ITransactionRepository transactionRepo,
                                   IDialogService dialogService)
        {
            _categoryRepo = categoryRepo;
            _transactionRepo = transactionRepo;
            _navigator = navigator;
            _dialogService = dialogService;

            SetCategoryCommand = new RelayCommand<int>((categoryID) => SetCategory(categoryID));
            AddCategoryCommand = new RelayCommand(AddCategory);

            Categories = new ObservableCollection<Category>(_categoryRepo.FindAll());
            Transactions = new ObservableCollection<Transaction>(_transactionRepo.GetAllSpendings().Where(x => x.CategoryID == 0));

            Remaining = Transactions.Count.ToString();

            SetCurrentTransaction();
        }

        private void SetCurrentTransaction()
        {
            if (Transactions.Count != 0)
            {
                CurrentTransaction = Transactions.First();
            }
            else
            {
                _dialogService.ShowMessage("Er zijn geen ongecategoriseerde transacties meer gevonden.", "Melding", "Terug naar dashboard", () => _navigator.NavigateTo("Dashboard"));
            }
        }

        private void AddCategory()
        {
            _navigator.NavigateTo("AddCategory");
        }

        private void SetCategory(int categoryID)
        {
            categorizeTransaction(CurrentTransaction, categoryID, true);
            Transactions.Remove(CurrentTransaction);
            categorizeEqualTransactions(CurrentTransaction);

            Remaining = Transactions.Count.ToString();
            _transactionRepo.Commit();
            SetCurrentTransaction();
        }

        public void categorizeEqualTransactions(Transaction CurrentTransaction)
        {
            List<Transaction> list = Transactions.ToList();
            foreach (Transaction transaction in list)
            {
                if (transaction.CreditorName == CurrentTransaction.CreditorName
                    && transaction.CreditorNumber == CurrentTransaction.CreditorNumber)
                {
                    categorizeTransaction(transaction, CurrentTransaction.CategoryID);
                    Transactions.Remove(transaction);
                }
            }
        }

        private void categorizeTransaction(Transaction t, int categoryid, bool updatebudget = false)
        {
            t.CategoryID = categoryid;
            _transactionRepo.Edit(t);

            if(updatebudget)
            {
                Category c = _categoryRepo.FindByID(categoryid);
                if(c.Type == (int)CategoryType.Fixed)
                {
                    c.Budget += t.Amount;
                    _categoryRepo.Edit(c);
                    _categoryRepo.Commit();
                }
            }
        }
    }
}
