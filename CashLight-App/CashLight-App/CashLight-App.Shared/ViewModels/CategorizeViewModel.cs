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

        public RelayCommand<int> SetCategoryCommand { get; set; }
        public RelayCommand AddCategoryCommand { get; set; }

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
                return "Nog " + _remaining + " rekeningen te categoriseren.";
            }
            set
            {
                _remaining = value;
                RaisePropertyChanged(() => Remaining);
            }
        }

        public string HasCategories
        {
            get
            {
                if (Categories.Count > 0)
                    return "visible";
                else
                    return "collapsed";
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

            Categories = new ObservableCollection<Category>(_categoryRepo.FindAll());
            Accounts = new ObservableCollection<Account>(
                _accountRepo.FindAllSpending()
                    .Where(x => x.Category == null)
                    .OrderByDescending(x => x.TotalAmount)
            );

            Remaining = Accounts.Count.ToString();

            SetCurrentAccount();
        }

        private void SetCurrentAccount()
        {
            if (Accounts.Count != 0)
            {
                CurrentAccount = Accounts.First();
            }
            else
            {
                _dialogService.ShowMessage("Er zijn geen ongecategoriseerde uitgaven meer gevonden.", "Melding", "Terug naar dashboard", () => _navigator.NavigateTo("Dashboard"));
            }
        }

        private void AddCategory()
        {
            _navigator.NavigateTo("AddCategory");
        }

        private void SetCategory(int categoryID)
        {
            categorizeAccount(CurrentAccount, categoryID, true);
            //Accounts.Remove(CurrentAccount);
            //categorizeEqualAccounts(CurrentAccount);

            //Remaining = Transactions.Count.ToString();
            //_transactionRepo.Commit();
            //SetCurrentTransaction();
        }

        private void categorizeAccount(Account account, int categoryid, bool updatebudget = false)
        {
            //t.CategoryID = categoryid;
            //_accountRepo.Edit(t);
            //if (updatebudget)
            //{
            //    Category c = _categoryRepo.FindByID(categoryid);
            //    if (c.Type == (int)CategoryType.Fixed)
            //    {
            //        c.Budget += t.Amount;
            //        _categoryRepo.Edit(c);
            //        _categoryRepo.Commit();
            //    }
            //}

            Accounts.Remove(account);
            SetCurrentAccount();
            Remaining = Accounts.Count.ToString();
        }
    }
}
