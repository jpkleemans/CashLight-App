﻿using CashLight_App.Repositories.Interfaces;
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
        private ITransactionRepository _transactionRepository;
        private ICSVReaderService _CSVReader;
        private IBankConverterService _bankConverter;

        public UploadRepository(ITransactionRepository transactionRepository,
                                ICSVReaderService CSVReader,
                                IBankConverterService bankConverter)
        {
            _transactionRepository = transactionRepository;
            _CSVReader = CSVReader;
            _bankConverter = bankConverter;
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