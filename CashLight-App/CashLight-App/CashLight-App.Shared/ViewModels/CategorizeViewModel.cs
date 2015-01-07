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
        public RelayCommand ShowMoreInfoCommand { get; set; }

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
            ShowMoreInfoCommand = new RelayCommand(ShowMoreInfo);

            Categories = new ObservableCollection<Category>(_categoryRepo.FindAll());
            Accounts = new ObservableCollection<Account>(
                _accountRepo.FindAllSpending()
                    .Where(x => x.CategoryID == 0)
                    .OrderByDescending(x => x.TotalAmount)
            );

            Remaining = Accounts.Count.ToString();

            SetCurrentAccount();
        }
        private void ShowMoreInfo()
        {
            _dialogService.ShowMessage("Test met newlines \nHoi dit is een newline", "Transactie informatie");
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
            Account account = CurrentAccount;
            account.CategoryID = categoryID;

            _accountRepo.Add(account);
            _accountRepo.Commit();
            
            Accounts.Remove(CurrentAccount);
            SetCurrentAccount();
            Remaining = Accounts.Count.ToString();
        }
    }
}
