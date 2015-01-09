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
        private IAccountRepository _accountRepo;
        private INavigationService _navigator;
        private IDialogService _dialogService;

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Account> Accounts { get; set; }
        public ObservableCollection<Account> CategorizedAccounts { get; set; }

        public RelayCommand<int> SetCategoryCommand { get; set; }
        public RelayCommand AddCategoryCommand { get; set; }
        public RelayCommand ShowMoreInfoCommand { get; set; }
        public RelayCommand<int> DeleteCategoryCommand { get; set; }

        private Account _currentAccount;
        public Account CurrentAccount
        {
            get
            {
                return _currentAccount;
            }
            set
            {
                _currentAccount = value;
                RaisePropertyChanged(() => CurrentAccount);
            }
        }

        private string _remaining;
        public string Remaining
        {
            get
            {
                if (HasUncategorizedAccounts)
                {
                    return "Nog " + _remaining + " rekeningen te categoriseren.";
                }
                else
                {
                    return "Geen rekeningen om te categoriseren.";
                }
            }
            set
            {
                _remaining = value;
                RaisePropertyChanged(() => Remaining);
                RaisePropertyChanged(() => HasUncategorizedAccounts);
            }
        }

        public bool HasCategories
        {
            get
            {
                return (Categories.Count > 0);
            }
        }

        public bool HasUncategorizedAccounts
        {
            get
            {
                return (Accounts.Count > 0);
            }
        }

        public CategorizeViewModel(INavigationService navigator,
                                   ICategoryRepository categoryRepo,
                                   IAccountRepository accountRepo,
                                   IDialogService dialogService)
        {
            _categoryRepo = categoryRepo;
            _accountRepo = accountRepo;
            _navigator = navigator;
            _dialogService = dialogService;

            SetCategoryCommand = new RelayCommand<int>((categoryID) => SetCategory(categoryID));
            AddCategoryCommand = new RelayCommand(AddCategory);
            ShowMoreInfoCommand = new RelayCommand(ShowMoreInfo);
            DeleteCategoryCommand = new RelayCommand<int>((categoryID) => DeleteCategory(categoryID));

            Categories = new ObservableCollection<Category>(_categoryRepo.FindAll());
            Accounts = new ObservableCollection<Account>(
                _accountRepo.FindAllSpending()
                    .Where(x => x.CategoryID == 0)
                    .OrderByDescending(x => x.TransactionTotalAmount)
            );
            CategorizedAccounts = new ObservableCollection<Account>(_accountRepo.FindAll());

            Remaining = Accounts.Count.ToString();

            SetCurrentAccount();
        }
        private void ShowMoreInfo()
        {
            string transactions = "De huidige rekeningen zijn van " + _currentAccount.Name + ".\n\n";
            foreach (var trans in _currentAccount.Transactions)
            {
                transactions += trans.Date.ToString("dd MMM yyyy") + "  \t" + trans.Amount.ToString("c") + "    \t" + Shorten(trans.Description.ToString(), 55) + "\n";
            }
            transactions += "\nTotaal:\t\t" + _currentAccount.TransactionTotalAmount.ToString("c");
            _dialogService.ShowMessage(transactions, "Rekeningdetails");
        }

        private void SetCurrentAccount()
        {
            if (Accounts.Count != 0)
            {
                CurrentAccount = Accounts.First();
            }
            //else
            //{
            //    _dialogService.ShowMessage("Er zijn geen ongecategoriseerde uitgaven meer gevonden.", "Melding");
            //}
        }

        private void AddCategory()
        {
            _navigator.NavigateTo("AddCategory");
        }

        private void SetCategory(int categoryID)
        {
            Account account = CurrentAccount;
            account.CategoryID = categoryID;

            _accountRepo.Add(account);
            _accountRepo.Commit();

            Accounts.Remove(CurrentAccount);
            SetCurrentAccount();
            Remaining = Accounts.Count.ToString();
        }

        private void DeleteCategory(int categoryID)
        {
            Category category = new Category();
            category.CategoryID = categoryID;
            foreach (var account in _accountRepo.FindAll())
            {
                if (account.CategoryID == categoryID)
                {
                    _accountRepo.Delete(account);
                    Accounts.Add(account);
                }
            }
            _accountRepo.Commit();
            _categoryRepo.Delete(category);
            _categoryRepo.Commit();
            Categories.Remove(Categories.Where(x => x.CategoryID == categoryID).First());
        }
        /// <summary>
        /// Used to make string shorter to specific length.
        /// </summary>
        /// <param name="value">Input string</param>
        /// <param name="maxChars">Amount of chars</param>
        /// <returns></returns>
        public string Shorten(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "…";
        }
    }
}
