
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;

namespace CashLight_App.Models
{
    public class ImportantCategory : ObservableObject
    {
        private Category _category;
        private double _amount;
        private double _height;
        private int _percentageOfBudget;

        public int HeightOfBudget
        {
            get
            {
                int maxHeight = (int)Height - 80;

                if (PercentageOfBudget >= 100)
                {
                    return maxHeight;
                }

                int newheight = Convert.ToInt32(maxHeight * ((double)PercentageOfBudget / 100));

                return newheight;
            }
        }

        public Thickness MarginOfBudget
        {
            get
            {
                return new Thickness(10, (HeightOfBudget - 10), -45, 0);
            }
        }

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
