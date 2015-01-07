using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models
{
    public class Account : ObservableObject
    {
        public string Name { get; set; }

        public string Number { get; set; }

        public int CategoryID { get; set; }

        public List<Transaction> Transactions { get; set; }

        public int TransactionCount { get; set; }

        public double TotalAmount { get; set; }
    }
}
