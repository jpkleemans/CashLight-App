using CashLight_App.Services.SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Tables
{
    [Table("Category")]
    class CategoryTable
    {
        [PrimaryKey, AutoIncrement]
        public int CategoryID { get; set; }

        public string Name { get; set; }

        [OneToMany]
        public IEnumerable<TransactionTable> Transactions { get; set; }

        public CategoryTable()
        {

        }

        public CategoryTable(string name)
        {
            this.Name = name;
        }
    }
}
