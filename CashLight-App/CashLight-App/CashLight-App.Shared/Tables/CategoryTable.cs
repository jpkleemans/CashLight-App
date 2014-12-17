using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Tables
{
    [Table("Category")]
    public class CategoryTable
    {
        public CategoryTable()
        {

        }


        public CategoryTable(string naam)
        {
            this.Naam = naam;
        }
        [PrimaryKey, AutoIncrement]
        public int CategoryID { get; set; }
        public string Naam { get; set; }

        [OneToMany]  
        public List<Transaction> Transacties { get; set; }

        public override string ToString()
        {
            return Naam;
        }
    }
}
