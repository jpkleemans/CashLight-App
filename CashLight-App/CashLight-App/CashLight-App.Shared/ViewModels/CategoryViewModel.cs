using CashLight_App.Enums;
using CashLight_App.Models;
using CashLight_App.Repositories.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {

        private ICategoryRepository _categoryRepo;
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

                if ((_currentType == "Other"))
                {
                    BudgetEnabled = "Collapsed";
                    Budget = 0;
                }
                else
                {
                    BudgetEnabled = "Visible";
                }

                RaisePropertyChanged(() => CurrentType);
            }
        }

        private string _budgetEnabled;
        public string BudgetEnabled
        {
            get
            {
                return _budgetEnabled;
            }
            set
            {
                _budgetEnabled = value;

                RaisePropertyChanged(() => BudgetEnabled);
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


        private double _budget;

        public double Budget
        {
            get
            {
                return _budget;
            }
            set
            {
                _budget = value;
                RaisePropertyChanged(() => Budget);
            }
        }



        public CategoryViewModel(ICategoryRepository categoryRepo)
        {

            _categoryRepo = categoryRepo;

            this.TypeList = new List<string>();
            this.TypeList.Add(CategoryType.Fixed.ToString());
            this.TypeList.Add(CategoryType.Other.ToString());
            this.TypeList.Add(CategoryType.Variable.ToString());

            BudgetEnabled = "Collapsed";

            SaveCategoryCommand = new RelayCommand(SaveCategory);

        }

        private void SaveCategory()
        {

            Category category = new Category();

            category.Name = this.Text;
            category.Type = (int)Enum.Parse(typeof(CategoryType), CurrentType);
            category.Budget = this.Budget;


            _categoryRepo.Add(category);

        }

    }
}
