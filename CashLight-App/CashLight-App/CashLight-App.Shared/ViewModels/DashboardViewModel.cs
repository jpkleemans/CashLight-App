using CashLight_App.DataModels;
using CashLight_App.Models;
using CashLight_App.Models.Interfaces;
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
        private IPeriodModel _periodModel;

        public ObservableCollection<TransactionModel> ImportantIncomes { get; set; }
        public ObservableCollection<TransactionModel> ImportantSpendings { get; set; }

        public DashboardViewModel(IUnitOfWork unitOfWork, IPeriodModel periodModel)
        {
            ImportantIncomes = new ObservableCollection<TransactionModel>();
            ImportantSpendings = new ObservableCollection<TransactionModel>();

            _unitOfWork = unitOfWork;
            _periodModel = periodModel;

            InitTransactions();
            InitSpendings();
        }

        public void InitTransactions()
        {
            if (IsInDesignMode)
            {
                ImportantIncomes.Add(new TransactionModel()
                {
                    Datum = new DateTime(2014, 10, 01),
                    Naam = "Test",
                    Bedrag = 100,
                    Height = 300
                });
                ImportantIncomes.Add(new TransactionModel()
                {
                    Datum = new DateTime(2014, 10, 05),
                    Naam = "Test2",
                    Bedrag = 350,
                    Height = 300
                });
                ImportantIncomes.Add(new TransactionModel()
                {
                    Datum = new DateTime(2014, 10, 11),
                    Naam = "Test3",
                    Bedrag = 149.99,
                    Height = 300
                });
                ImportantIncomes.Add(new TransactionModel()
                {
                    Datum = new DateTime(2014, 10, 28),
                    Naam = "Test4",
                    Bedrag = 199,
                    Height = 300
                });
            }
            else
            {
                ImportantIncomes = new ObservableCollection<TransactionModel>(_periodModel.getMostImportantIncomes());
            }
        }

        public void InitSpendings()
        {
            if (IsInDesignMode)
            {
                ImportantSpendings.Add(new TransactionModel()
                {
                    Datum = new DateTime(2014, 10, 01),
                    Naam = "Test",
                    Bedrag = 100,
                    Height = 300
                });
                ImportantSpendings.Add(new TransactionModel()
                {
                    Datum = new DateTime(2014, 10, 05),
                    Naam = "Test2",
                    Bedrag = 350,
                    Height = 300
                });
                ImportantSpendings.Add(new TransactionModel()
                {
                    Datum = new DateTime(2014, 10, 11),
                    Naam = "Test3",
                    Bedrag = 149.99,
                    Height = 300
                });
                ImportantSpendings.Add(new TransactionModel()
                {
                    Datum = new DateTime(2014, 10, 28),
                    Naam = "Test4",
                    Bedrag = 199,
                    Height = 300
                });
            }
            else
            {
                ImportantIncomes = new ObservableCollection<TransactionModel>(_periodModel.getMostImportantSpendings());
            }
        }
    }
}
