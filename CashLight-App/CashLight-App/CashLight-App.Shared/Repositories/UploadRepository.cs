using CashLight_App.Repositories.Interfaces;
using CashLight_App.Enums;
using CashLight_App.Models;
using CashLight_App.Services.CSV;
using CashLight_App.Tables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Windows.Storage;
using CashLight_App.Services.CSV.Banks;

namespace CashLight_App.Repositories
{
    class UploadRepository : IUploadRepository
    {
        private ITransactionRepository _transactionRepository;
        private ICSVService _CSVService;

        public UploadRepository(ITransactionRepository transactionRepository, ICSVService CSVService)
        {
            _transactionRepository = transactionRepository;
            _CSVService = CSVService;
        }

        public async void ToDatabase(IBank bank, StorageFile storageFile)
        {
            Stream stream = await storageFile.OpenStreamForReadAsync();

            List<Dictionary<string, string>> list;

            list = _CSVService.ReadToList(stream, bank);

            foreach (Dictionary<string, string> dic in list)
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

                bool exists = _transactionRepository.Exists(transaction);

                if (!exists)
                {
                    _transactionRepository.Add(transaction);
                }

            }
            _transactionRepository.Commit();
        }
    }
}
