using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models
{
    public class Period : ObservableObject
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private IEnumerable<Transaction> _transactions;
        private IEnumerable<Transaction> _importantIncomes;
        private IEnumerable<ImportantCategory> _importantSpendingCategories;
        //private IEnumerable<Category> _categories;
        private double _spendingsLimit;

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
                RaisePropertyChanged(() => StartDate);
            }
        }

        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
                RaisePropertyChanged(() => EndDate);
            }
        }

        public IEnumerable<Transaction> Transactions
        {
            get
            {
                return _transactions;
            }
            set
            {
                _transactions = value;
                RaisePropertyChanged(() => Transactions);
            }
        }

        public IEnumerable<Transaction> ImportantIncomes
        {
            get
            {
                return _importantIncomes;
            }
            set
            {
                _importantIncomes = value;
                RaisePropertyChanged(() => ImportantIncomes);
            }
        }

        public IEnumerable<ImportantCategory> ImportantSpendingCategories
        {
            get
            {
                return _importantSpendingCategories;
            }
            set
            {
                _importantSpendingCategories = value;
                RaisePropertyChanged(() => ImportantSpendingCategories);
            }
        }

        //public IEnumerable<Category> Categories
        //{
        //    get
        //    {
        //        return _categories;
        //    }
        //    set
        //    {
        //        _categories = value;
        //        RaisePropertyChanged(() => Categories);
        //    }
        //}

        public double SpendingsLimit
        {
            get
            {
                return _spendingsLimit;
            }
            set
            {
                _spendingsLimit = value;
                RaisePropertyChanged(() => SpendingsLimit);
            }
        }
    }
}
