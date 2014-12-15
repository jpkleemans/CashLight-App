using CashLight_App.DataModels;
using CashLight_App.Enums;
using CashLight_App.Models.Interfaces;
using CashLight_App.Services.CSV;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Windows.Storage;
using Windows.Storage.Streams;

namespace CashLight_App.Models
{
    public class UploadModel : ModelBase
    {
        private TransactionModel _transaction;

        public UploadModel()
        {
            _transaction = new TransactionModel();
        }

        public async void ToDatabase(IBank bank, StorageFile storageFile)
        {
            Stream stream = await storageFile.OpenStreamForReadAsync();

            CsvFileReader reader = new CsvFileReader(bank, stream);

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

                    var transaction = new Transaction()
                    {
                        AfBij = (int)Enum.Parse(typeof(AfBij), dic["Af / Bij"]),
                        Bedrag = Double.Parse(dic["Bedrag (EUR)"], new CultureInfo("nl-NL")),
                        Code = 0,
                        Tegenrekening = dic["Tegenrekening"],
                        Mededelingen = dic["Mededelingen"],
                        Naam = dic["Naam / Omschrijving"],
                        Rekening = dic["Rekening"],
                        Datum = csvDate
                    };

                    _unitOfWork.Transaction.Add(transaction);

                }

            }
            _unitOfWork.Commit();

            //PeriodModel.SearchMostConsistentIncome();
        }
    }
}
