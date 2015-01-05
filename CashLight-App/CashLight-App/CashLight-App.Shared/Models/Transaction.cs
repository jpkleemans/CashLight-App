using GalaSoft.MvvmLight;
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
        private int _transactionID;

        public int TransactionID
        {
            get
            {
                return _transactionID;
            }
            private set
            {
                _transactionID = value;
                RaisePropertyChanged(() => _transactionID);
            }
        }

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
        /// <summary>
        /// Je eigen rekeningnummer.
        /// </summary>
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
        /// <summary>
        /// Is de naam van de tegenrekening.
        /// </summary>
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
        /// <summary>
        /// Tegenrekeningnummer
        /// </summary>
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
        /// <summary>
        /// Betalingskenmerk code
        /// </summary>
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
        /// <summary>
        /// Af-Bij, plus of min.
        /// </summary>
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
        /// <summary>
        /// Hoeveelheid van de transacties.
        /// </summary>
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
        /// <summary>
        /// Omschrijving van de transactie.
        /// </summary>
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

        /// <summary>
        /// CategorieID
        /// </summary>
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
