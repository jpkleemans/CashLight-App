using CashLight_App.Enums;
using CashLight_App.Services.SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Tables
{
    [Table("Category")]
    public class CategoryTable
    {
        [PrimaryKey, AutoIncrement]
        public int CategoryID { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public double Budget { get; set; }

        [OneToMany]
        public IEnumerable<TransactionTable> Transactions { get; set; }

        public CategoryTable()
        {

        }

        public CategoryTable(string name, CategoryType type, double budget)
        {
            this.Name = name;
            this.Type = (int)type;
            this.Budget = Budget;
        }
    }
}
