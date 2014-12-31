using CashLight_App.Services.SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Tables
{
    [Table("Transaction")]
    class TransactionTable
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionID { get; set; }

        public DateTime Date { get; set; }
        public string DebtorNumber { get; set; }
        public string CreditorName { get; set; }
        public string CreditorNumber { get; set; }
        public int Code { get; set; }
        public int InOut { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }

        [ForeignKey(typeof(CategoryTable))]
        public int CategoryID { get; set; }

        [ManyToOne]
        public virtual CategoryTable Category { get; set; }

        public TransactionTable()
        {

        }

        public TransactionTable(DateTime date, string debtorNumber, string creditorName, string creditorNumber, int code, int inOut, double amount, string description, int categoryID)
        {
            this.Date = date;
            this.DebtorNumber = debtorNumber;
            this.CreditorName = creditorName;
            this.CreditorNumber = creditorNumber;
            this.Code = code;
            this.InOut = inOut;
            this.Amount = amount;
            this.Description = description;
            this.CategoryID = categoryID;
        }

    }
}
