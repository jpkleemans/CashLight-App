using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.DataModel
{
    [Table("Category")]
    public class Category
    {
        public Category()
        {

        }


        public Category(string naam)
        {
            this.Naam = naam;
        }
        [PrimaryKey, AutoIncrement]
        public int CategoryID { get; set; }
        public string Naam { get; set; }

        //public virtual List<Transaction> Transacties { get; set; }

        public override string ToString()
        {
            return Naam;
        }
    }
}
