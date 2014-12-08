using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Model
{
    public class Transaction
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public Transaction(DateTime date, string name, double amount)
        {
            Date = date;
            Name = name;
            Amount = amount;
        }
    }
}
