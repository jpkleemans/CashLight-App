﻿using CashLight_App.Models;
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

        public RelayCommand<int> SetCategoryCommand { get; set; }

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

        public CategorizeViewModel(ICategoryRepository categoryRepo, ITransactionRepository transactionRepo)
        {
            _categoryRepo = categoryRepo;
            _transactionRepo = transactionRepo;

            SetCategoryCommand = new RelayCommand<int>((categoryID) => SetCategory(categoryID));

            Categories = new ObservableCollection<Category>(_categoryRepo.FindAll());
            Transactions = new ObservableCollection<Transaction>(_transactionRepo.GetAllSpendings());

            //If no transactions present, then show nothing. Instead of crashing.
            if (Transactions.Count != 0)
            {
                CurrentTransaction = Transactions.First();
            }
            else
            {
                Debug.WriteLine("No transactions! Panic!");
            }
            Remaining = Transactions.Count.ToString();
        }

        private void SetCategory(int categoryID)
        {
            CurrentTransaction.CategoryID = categoryID;
            _transactionRepo.Edit(CurrentTransaction);
            _transactionRepo.Commit();
            Transactions.Remove(CurrentTransaction);
            removeEqualTransactions(CurrentTransaction);
            Remaining = Transactions.Count.ToString();
            CurrentTransaction = Transactions.First();
        }

        public void removeEqualTransactions(Transaction CurrentTransaction)
        {
            List<Transaction> list = Transactions.ToList();
            foreach (Transaction transaction in list)
            {
                if (transaction.CreditorName == CurrentTransaction.CreditorName && transaction.CreditorNumber == CurrentTransaction.CreditorNumber)
                {
                    transaction.CategoryID = CurrentTransaction.CategoryID;
                    Transactions.Remove(transaction);
                }
            }
        }
    }
}
