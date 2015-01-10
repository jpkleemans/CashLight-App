
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
        private double _amountOfBudget;

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
                int height = 15;

                if (HeightOfBudget > height)
                {
                    height = HeightOfBudget;
                }

                int marginLeft;
                if (AmountOfBudget >= 1000)
                    marginLeft = -100;
                else if (AmountOfBudget >= 100)
                    marginLeft = -80;
                else if (AmountOfBudget >= 10)
                    marginLeft = -70;
                else
                    marginLeft = -60;

                return new Thickness(10, (height - 10), marginLeft, 0);
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


        public double AmountOfBudget
        {
            get
            {
                return _amountOfBudget;
            }
            set
            {
                _amountOfBudget = value;
                RaisePropertyChanged(() => AmountOfBudget);
            }
        }
    }
}
