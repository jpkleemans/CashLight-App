using CashLight_App.Enums;
using CashLight_App.Models;
using CashLight_App.Repositories.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {
        private ICategoryRepository _categoryRepo;
        private INavigationService _navigator;
        private IDialogService _dialogService;

        public RelayCommand SaveCategoryCommand { get; set; }

        private List<string> _typeList;

        /// <summary>
        /// TypeList get / set
        /// </summary>
        public List<string> TypeList
        {
            get
            {
                return _typeList;
            }
            set
            {
                _typeList = value;
                RaisePropertyChanged(() => TypeList);
            }
        }


        private string _currentType;
        /// <summary>
        /// CurrentType get / set
        /// </summary>
        public string CurrentType
        {
            get
            {
                return _currentType;
            }
            set
            {
                _currentType = value;

                if (_currentType == "Fixed")
                {
                    _budget = null;
                }

                RaisePropertyChanged(() => CurrentType);
                RaisePropertyChanged(() => BudgetEnabled);
            }
        }

        /// <summary>
        /// BudgetEnabled get / set
        /// </summary>
        public bool BudgetEnabled
        {
            get
            {
                if (CurrentType == "Variable")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private string _text;
        /// <summary>
        /// Test get / set
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                RaisePropertyChanged(() => Text);
            }
        }

        private double? _budget;
        /// <summary>
        /// Budget get / set
        /// </summary>
        public string Budget
        {
            get
            {
                return _budget.ToString();
            }
            set
            {
                _budget = double.Parse(value);
                RaisePropertyChanged(() => Budget);
            }
        }

        /// <summary>
        /// Initialiseert de klasse met de juiste dependencies
        /// </summary>
        /// <param name="categoryRepo">categoryRepo</param>
        /// <param name="navigator">Navigator</param>
        /// <param name="dialogService">DialogService</param>
        public CategoryViewModel(ICategoryRepository categoryRepo, INavigationService navigator, IDialogService dialogService)
        {
            _navigator = navigator;
            _categoryRepo = categoryRepo;
            _dialogService = dialogService;

            this.TypeList = new List<string>();
            this.TypeList.Add(CategoryType.Fixed.ToString());
            this.TypeList.Add(CategoryType.Variable.ToString());

            SaveCategoryCommand = new RelayCommand(SaveCategory);
        }

        /// <summary>
        /// Opslaan van de categorie
        /// </summary>
        private async void SaveCategory()
        {
            Category category = new Category();

            if (Text == null)
            {
                await _dialogService.ShowError("De categorie-naam is niet ingevuld.", "Ongeldige naam", "Terug", null);
            }
            else if (CurrentType == null)
            {
                await _dialogService.ShowError("Het categorie-type is niet ingevuld.", "Ongeldig type", "Terug", null);
            }
            else
            {

                category.Name = this.Text;
                category.Type = (int)Enum.Parse(typeof(CategoryType), CurrentType);

                if (_budget != null)
                {
                    category.Budget = (double)_budget;
                }
                else
                {
                    category.Budget = 0;
                }

                // if alright
                _categoryRepo.Add(category);
                _navigator.NavigateTo("Categorize");

            }

        }

    }
}
