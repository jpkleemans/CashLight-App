using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Model
{
    public class TransactionModel
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public TransactionModel(DateTime date, string name, double amount)
        {
            Date = date;
            Name = name;
            Amount = amount;
        }
    }
}
