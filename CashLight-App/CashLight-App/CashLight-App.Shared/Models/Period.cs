using CashLight_App.Models.Interface;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models
{
    class Period : ObservableObject, IPeriod
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private List<ITransaction> _transactions;
        private List<ITransaction> _importantIncomes;
        private List<ITransaction> _importantSpendings;
        private List<ICategory> _categories;

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

        public List<ITransaction> Transactions
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

        public List<ITransaction> ImportantIncomes
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

        public List<ITransaction> ImportantSpendings
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

        public List<ICategory> Categories
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
