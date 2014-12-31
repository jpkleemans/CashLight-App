using GalaSoft.MvvmLight;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models
{
    public class Transaction : ObservableObject
    {
        private DateTime _date;
        private string _debtorNumber;
        private string _creditorName;
        private string _creditorNumber;
        private int _code;
        private int _inOut;
        private double _amount;
        private string _description;
        private Category _category;
        private double _height;
        private int _categoryID;

        public DateTime Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                RaisePropertyChanged(() => Date);
            }
        }

        public string DebtorNumber
        {
            get
            {
                return _debtorNumber;
            }
            set
            {
                _debtorNumber = value;
                RaisePropertyChanged(() => DebtorNumber);
            }
        }

        public string CreditorName
        {
            get
            {
                return _creditorName;
            }
            set
            {
                _creditorName = value;
                RaisePropertyChanged(() => CreditorName);
            }
        }

        public string CreditorNumber
        {
            get
            {
                return _creditorNumber;
            }
            set
            {
                _creditorNumber = value;
                RaisePropertyChanged(() => CreditorNumber);
            }
        }

        public int Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
                RaisePropertyChanged(() => Code);
            }
        }

        public int InOut
        {
            get
            {
                return _inOut;
            }
            set
            {
                _inOut = value;
                RaisePropertyChanged(() => InOut);
            }
        }

        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                _amount = value;
                RaisePropertyChanged(() => Amount);
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
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


        public int CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
                RaisePropertyChanged(() => CategoryID);
            }
        }
    }
}
