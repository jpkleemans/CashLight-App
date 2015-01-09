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

        public RelayCommand SaveCategoryCommand { get; set; }

        private List<string> _typeList;
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

        public CategoryViewModel(ICategoryRepository categoryRepo, INavigationService navigator)
        {
            _navigator = navigator;
            _categoryRepo = categoryRepo;

            this.TypeList = new List<string>();
            this.TypeList.Add(CategoryType.Fixed.ToString());
            this.TypeList.Add(CategoryType.Variable.ToString());

            SaveCategoryCommand = new RelayCommand(SaveCategory);
        }

        private void SaveCategory()
        {
            Category category = new Category();

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

            _categoryRepo.Add(category);

            _navigator.NavigateTo("Categorize");
        }

    }
}
