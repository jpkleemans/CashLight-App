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
        private List<Transaction> _transactions;
        private List<Transaction> _importantIncomes;
        private List<Transaction> _importantSpendings;
        private List<Category> _categories;

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

        public List<Transaction> Transactions
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

        public List<Transaction> ImportantIncomes
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

        public List<Transaction> ImportantSpendings
        {
            get
            {
                return _importantSpendings;
            }
            set
            {
                _importantSpendings = value;
                RaisePropertyChanged(() => ImportantSpendings);
            }
        }

        public List<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                RaisePropertyChanged(() => Categories);
            }
        }
    }
}
