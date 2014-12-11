using CashLight.Database;
using CashLight.Database.DataModels;
using CashLight.Database.Interfaces;
using CashLight.Enums;
using CashLight.Model.Interface;
using CashLight.Utility.CSV;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CashLight.Model
{
    public class Upload
    {
        private string _filePath;
        private Transaction _transaction;

        public Upload(string filePath)
        {
            _filePath = filePath;
            _transaction = new Transaction();
        }

        public void ToDatabase(IBank bank)
        {
            CsvFileReader reader = new CsvFileReader(bank, _filePath);

            List<Dictionary<string, string>> list;
            //try
            //{
                list = reader.ReadToList();
            //}
            //catch (Exception)
            //{
            //    list = new List<Dictionary<string, string>>();

            //    MessageBox.Show(
            //        "Ongeldig CSV bestand",
            //        "Foutmelding",
            //        MessageBoxButtons.OK,
            //        MessageBoxIcon.Error,
            //        MessageBoxDefaultButton.Button1
            //    );

            //    Application.Exit();
            //}

            foreach (Dictionary<string, string> dic in list)
            {
                
                DateTime csvDate = Convert.ToDateTime(dic["Datum"]);
                
                bool exists = _transaction.Exists(dic);
                if (exists == false)
                {

                var transaction = new CashLight.Database.DataModels.Transaction()
                {
                    AfBij = (int)Enum.Parse(typeof(AfBij), dic["Af / Bij"]),
                    Bedrag = Double.Parse(dic["Bedrag (EUR)"]),
                    Code = 0,
                    Tegenrekening = dic["Tegenrekening"],
                    Mededelingen = dic["Mededelingen"],
                    Naam = dic["Naam / Omschrijving"],
                    Rekening = dic["Rekening"],
                    Datum = csvDate
                };

                Kernel.Database.Transaction.Add(transaction);

            }

            }
            Kernel.Database.Commit();
            Period period = new Period();
            Hashtable Income = period.GetMostConsistentIncome();          
        }
    }
}
