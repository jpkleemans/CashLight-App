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
        public ObservableCollection<Account> UncategorizedAccounts { get; set; }
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
                    return "Geen rekeningen om te categoriseren. Klik met uw rechtermuisknop om het menu te openen.";
                }
            }
            set
            {
                _remaining = value;
                RaisePropertyChanged(() => Remaining);
                RaisePropertyChanged(() => CategoryListLabel);
                RaisePropertyChanged(() => HasUncategorizedAccounts);
            }
        }

        /// <summary>
        /// Haalt de juiste tekst op
        /// </summary>
        public string CategoryListLabel
        {
            get
            {
                if (HasUncategorizedAccounts)
                {
                    return "Alle uitgaven van deze rekening horen bij de categorie:";
                }
                else
                {
                    return "Uw categorie(ën):";
                }
            }
        }

        public bool HasCategories { get { return (Categories.Count > 0); } }
        public bool HasUncategorizedAccounts { get { return (UncategorizedAccounts.Count > 0); } }

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
            UncategorizedAccounts = new ObservableCollection<Account>(
                _accountRepo.FindAll()
                    .Where(x => x.CategoryID == 0)
                    .OrderByDescending(x => x.TransactionTotalAmount)
            );
            CategorizedAccounts = new ObservableCollection<Account>(_accountRepo.FindAllCategorized());

            Remaining = UncategorizedAccounts.Count.ToString();

            SetCurrentAccount();
        }

        /// <summary>
        /// Laat info zien
        /// </summary>
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

        /// <summary>
        /// Registreert de huidige rekening.
        /// </summary>
        private void SetCurrentAccount()
        {
            if (UncategorizedAccounts.Count != 0)
            {
                CurrentAccount = UncategorizedAccounts.First();
            }
            else
            {
                CurrentAccount = null;
            }
        }

        /// <summary>
        /// Voegt een categorie toe
        /// </summary>
        private void AddCategory()
        {
            _navigator.NavigateTo("AddCategory");
        }

        /// <summary>
        /// Koppelen van een account aan een categorie
        /// </summary>
        /// <param name="categoryID">CategoryID</param>
        private void SetCategory(int categoryID)
        {
            if (CurrentAccount != null)
            {
                Category category = _categoryRepo.FindByID(categoryID);

                Account account = CurrentAccount;
                account.CategoryID = category.CategoryID;

                _accountRepo.Add(account);
                _accountRepo.Commit();

                if (category.Type == (int)CategoryType.Fixed)
                {
                    Transaction transaction = account.Transactions.FirstOrDefault();
                    if (transaction != null)
                    {
                        category.Budget += transaction.Amount;
                        _categoryRepo.Edit(category);
                        _categoryRepo.Commit();
                    }
                }

                CategorizedAccounts.Add(account);
                UncategorizedAccounts.Remove(account);

                SetCurrentAccount();
                Remaining = UncategorizedAccounts.Count.ToString();
            }
        }

        /// <summary>
        /// Verwijdert een categorie
        /// </summary>
        /// <param name="categoryID">CategoryID</param>
        private void DeleteCategory(int categoryID)
        {
            Category category = Categories.Where(x => x.CategoryID == categoryID).First();

            foreach (var account in _accountRepo.FindAll())
            {
                if (account.CategoryID == category.CategoryID)
                {
                    _accountRepo.Delete(account);
                    UncategorizedAccounts.Add(account);
                }
            }
            _accountRepo.Commit();

            List<Account> categorizedAccounts = CategorizedAccounts.Where(x => x.CategoryID == category.CategoryID).ToList();
            foreach (Account categorizedAccount in categorizedAccounts)
            {
                CategorizedAccounts.Remove(categorizedAccount);
            }

            _categoryRepo.Delete(category);
            _categoryRepo.Commit();
            Categories.Remove(category);

            SetCurrentAccount();
            Remaining = UncategorizedAccounts.Count.ToString();
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
