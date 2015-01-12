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
using GalaSoft.MvvmLight.Views;
using Windows.UI.Xaml;

namespace CashLight_App.Repositories
{
    public class UploadRepository : IUploadRepository
    {
        private ITransactionRepository _transactionRepo;
        private ICSVReaderService _CSVReader;
        private IBankConverterService _bankConverter;
        private IPeriodRepository _periodRepo;
        private IDialogService _dialogService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transactionRepo"></param>
        /// <param name="CSVReader"></param>
        /// <param name="bankConverter"></param>
        /// <param name="periodRepo"></param>
        /// <param name="dialogService"></param>
        public UploadRepository(ITransactionRepository transactionRepo,
                                ICSVReaderService CSVReader,
                                IBankConverterService bankConverter,
                                IPeriodRepository periodRepo, IDialogService dialogService)
        {
            _transactionRepo = transactionRepo;
            _CSVReader = CSVReader;
            _bankConverter = bankConverter;
            _periodRepo = periodRepo;
            _dialogService = dialogService;
        }

        /// <summary>
        /// ToDatabase saves csv to database
        /// </summary>
        /// <param name="storageFile"></param>
        public async void ToDatabase(StorageFile storageFile)
        {
            bool crashChecker = false;
            try
            {
                List<Dictionary<string, string>> bankList = await CreateList(storageFile);

                foreach (Dictionary<string, string> dic in bankList)
                {
                    SaveTransaction (dic);
                }
            }
            catch (Exception)
            {

                crashChecker = true;

            }
            if (crashChecker)
            {
                await _dialogService.ShowError("Het CSV-bestand is ongeldig. \nDownload een nieuw CSV-bestand van Mijn ING.", "Ongeldig CSV", "App afsluiten", () => Application.Current.Exit());
            }
            _transactionRepo.Commit();

            _periodRepo.SearchMostConsistentIncome();
        }

        /// <summary>
        /// Saves a single transaction
        /// </summary>
        /// <param name="dic"></param>
        public void SaveTransaction(Dictionary<string, string> dic)
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

        /// <summary>
        /// Creates a list of the csv (storageFile)
        /// </summary>
        /// <param name="storageFile"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task<List<Dictionary<string, string>>> CreateList(StorageFile storageFile)
        {
            Stream stream = await storageFile.OpenStreamForReadAsync();

            List<List<string>> csvList = _CSVReader.ReadToList(stream);

            List<Dictionary<string, string>> bankList = _bankConverter.Convert(csvList);
            return bankList;
        }
    }
}
