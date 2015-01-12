using CashLight_App.Services.SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Tables
{
    [Table("AccountCategory")]
    public class AccountCategoryTable
    {
        [PrimaryKey, AutoIncrement]
        public int AccountCategoryID { get; set; }

        public string Number { get; set; }

        public string Name { get; set; }

        [ForeignKey(typeof(CategoryTable))]
        public int CategoryID { get; set; }

        [ManyToOne]
        public virtual CategoryTable Category { get; set; }
    }
}
