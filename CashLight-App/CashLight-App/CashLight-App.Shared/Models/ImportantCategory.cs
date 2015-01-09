
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models
{
    public class ImportantCategory : ObservableObject
    {
        private Category _category;
        private double _amount;
        private int _percentageOfBudget;
        private double _height;
        public Category Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
                RaisePropertyChanged(() => Category);
            }
        }

        public int PercentageOfBudget
        {
            get
            {
                return _percentageOfBudget;
            }
            set
            {
                _percentageOfBudget = value;
                RaisePropertyChanged(() => PercentageOfBudget);
            }
        }

        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                RaisePropertyChanged(() => Height);
            }
        }

    }
}
