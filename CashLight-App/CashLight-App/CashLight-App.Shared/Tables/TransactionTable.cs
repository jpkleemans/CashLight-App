﻿using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Tables
{
    [Table("Transaction")]
    public class TransactionTable
    {

        public TransactionTable()
        {

        }
        public TransactionTable(DateTime datum, string naam, string rekening, string tegenrekening, int code, int afbij, double bedrag, string mededelingen, int categoryid)
        {
            this.Datum = datum;
            this.Naam = naam;
            this.Rekening = rekening;
            this.Tegenrekening = tegenrekening;
            this.Code = code;
            this.AfBij = afbij;
            this.Bedrag = bedrag;
            this.Mededelingen = mededelingen;
            this.CategoryID = categoryid;
        }
        //primary key
        [PrimaryKey, AutoIncrement]
        public int TransactionID { get; set; }
        public DateTime Datum { get; set; }
        public string Naam { get; set; }
        public string Rekening { get; set; }
        public string Tegenrekening { get; set; }
        public int Code { get; set; }
        public int AfBij { get; set; }
        public double Bedrag { get; set; }
        public string Mededelingen { get; set; }
        [ForeignKey(typeof(CategoryTable))]
        public int CategoryID { get; set; }
        [ManyToOne]
        public virtual CategoryTable Category { get; set; }

    }
}
