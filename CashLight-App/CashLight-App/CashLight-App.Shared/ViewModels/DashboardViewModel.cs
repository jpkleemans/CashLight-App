using CashLight_App.DataModels;
using CashLight_App.Models;
using CashLight_App.Services.Interface;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CashLight_App.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private IUnitOfWork _unitOfWork;

        public ObservableCollection<TransactionModel> ImportantIncomes { get; set; }

        public DashboardViewModel(IUnitOfWork unitOfWork)
        {
            ImportantIncomes = new ObservableCollection<TransactionModel>();

            _unitOfWork = unitOfWork;

            InitTransactions();
        }

        public void InitTransactions()
        {
            if (IsInDesignMode)
            {
                //ImportantIncomes.Add(new Transaction()
                //{
                //    Datum = new DateTime(2014, 10, 01),
                //    Naam = "Test",
                //    Bedrag = 100
                //});
                //ImportantIncomes.Add(new Transaction()
                //{
                //    Datum = new DateTime(2014, 10, 05),
                //    Naam = "Test2",
                //    Bedrag = 350
                //});
                //ImportantIncomes.Add(new Transaction()
                //{
                //    Datum = new DateTime(2014, 10, 11),
                //    Naam = "Test3",
                //    Bedrag = 149.99
                //});
                //ImportantIncomes.Add(new Transaction()
                //{
                //    Datum = new DateTime(2014, 10, 28),
                //    Naam = "Test4",
                //    Bedrag = 199
                //});
            }
            else
            {
                IEnumerable<TransactionModel> all = TransactionModel.All();
                List<TransactionModel> mostImportantIncomes = TransactionModel.getMostImportantTransactionsBij(all.ToList(), new DateTime(2014, 10, 01), new DateTime(2014, 11, 01));

                foreach (TransactionModel item in mostImportantIncomes)
                {
                    ImportantIncomes.Add(item);
                }
            }
        }
    }
}
