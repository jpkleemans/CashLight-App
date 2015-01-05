using CashLight_App.Repositories.Interfaces;
using CashLight_App.Enums;
using CashLight_App.Models;
using CashLight_App.Services.CSVReader;
using CashLight_App.Tables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Windows.Storage;
using CashLight_App.Services.BankConverter;

namespace CashLight_App.Repositories
{
    class UploadRepository : IUploadRepository
    {
        private ITransactionRepository _transactionRepo;
        private ICSVReaderService _CSVReader;
        private IBankConverterService _bankConverter;
        private IPeriodRepository _periodRepo;

        public UploadRepository(ITransactionRepository transactionRepo,
                                ICSVReaderService CSVReader,
                                IBankConverterService bankConverter,
                                IPeriodRepository periodRepo)
        {
            _transactionRepo = transactionRepo;
            _CSVReader = CSVReader;
            _bankConverter = bankConverter;
            _periodRepo = periodRepo;
        }

        public async void ToDatabase(StorageFile storageFile)
        {
            Stream stream = await storageFile.OpenStreamForReadAsync();

            List<List<string>> csvList = _CSVReader.ReadToList(stream);

            List<Dictionary<string, string>> bankList = _bankConverter.Convert(csvList);

            foreach (Dictionary<string, string> dic in bankList)
            {
                int inOut;
                if (dic["Af / Bij"] == "Bij")
                {
                    inOut = (int)InOut.In;
                }
                else
                {
                    inOut = (int)InOut.Out;
                }

                DateTime csvDate = Convert.ToDateTime(dic["Datum"]);

                Transaction transaction = new Transaction()
                {
                    InOut = inOut,
                    Amount = Double.Parse(dic["Bedrag (EUR)"], new CultureInfo("nl-NL")),
                    Code = 0,
                    CreditorNumber = dic["Tegenrekening"],
                    Description = dic["Mededelingen"],
                    CreditorName = dic["Naam / Omschrijving"],
                    DebtorNumber = dic["Rekening"],
                    Date = csvDate
                };

                bool exists = _transactionRepo.Exists(transaction);

                if (!exists)
                {
                    _transactionRepo.Add(transaction);
                }

            }
            _transactionRepo.Commit();

            _periodRepo.SearchMostConsistentIncome();
        }
    }
}
