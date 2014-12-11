using CashLight_App.DataModels;
using CashLight_App.Enums;
using CashLight_App.Models.Interfaces;
using CashLight_App.Services.CSV;
using System;
using System.Collections.Generic;
using System.IO;
using Windows.Storage;

namespace CashLight_App.Models 
{
    public class UploadModel : ModelBase
    {
        private StorageFile _storageFile;
        private Stream _streamFile;
        private TransactionModel _transaction;

        public UploadModel(StorageFile storageFile)
        {
            _storageFile = storageFile;

            CsvConverter.ToStream(_storageFile);

            _streamFile = CsvConverter.GetResult();
            _transaction = new TransactionModel();
        }

        public void ToDatabase(IBank bank)
        {
            CsvFileReader reader = new CsvFileReader(bank, _streamFile);

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
                    Bedrag = Double.Parse(dic["Bedrag (EUR)"]),
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
        }
    }
}
