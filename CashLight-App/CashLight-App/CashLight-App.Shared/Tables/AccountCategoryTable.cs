using CashLight_App.Services.SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Tables
{
    [Table("AccountCategory")]
    class AccountCategoryTable
    {
        [PrimaryKey, AutoIncrement]
        public int AccountCategoryID { get; set; }

        public string AccountNumber { get; set; }

        [ForeignKey(typeof(CategoryTable))]
        public int CategoryID { get; set; }

        [ManyToOne]
        public virtual CategoryTable Category { get; set; }
    }
}
