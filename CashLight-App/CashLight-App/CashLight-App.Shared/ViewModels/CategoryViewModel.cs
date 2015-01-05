using CashLight_App.Enums;
using CashLight_App.Repositories.Interfaces;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.ViewModels
{
    public class CategoryViewModel : ViewModelBase
    {

        private ICategoryRepository _categoryRepo;

        private string _text;
        private List<string> _typeList;
        private string _currentType;

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

        public string CurrentType
        {
            get
            {
                return _currentType;
            }
            set
            {
                _currentType = value;

                if ((_currentType == "Fixed"))
                {
                    AmountEnabled = "Collapsed";
                }
                else
                {
                    AmountEnabled = "Visible";
                }

                RaisePropertyChanged(() => CurrentType);
            }
        }

        private string _amountEnabled;
        public string AmountEnabled
        {
            get
            {
                return _amountEnabled;
            }
            set
            {
                _amountEnabled = value;

                RaisePropertyChanged(() => AmountEnabled);
            }
        }

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

        public CategoryViewModel(ICategoryRepository categoryRepo)
        {
            this.TypeList = new List<string>();
            this.TypeList.Add(CategoryType.Fixed.ToString());
            this.TypeList.Add(CategoryType.Other.ToString());
            this.TypeList.Add(CategoryType.Variable.ToString());

            Text = "Hey hoi!";

        }

    }
}
