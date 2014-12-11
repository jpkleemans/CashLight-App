using CashLight_App.DataModels;
using CashLight_App.Models;
using CashLight_App.Models.Interfaces;
using CashLight_App.Services.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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

        public RelayCommand NextPeriodCommand;

        public RelayCommand PreviousPeriodCommand;

        public ObservableCollection<TransactionModel> ImportantIncomes { get; set; }
        public ObservableCollection<TransactionModel> ImportantSpendings { get; set; }

        public string[] IncomeCategories { get; set; }

        public string[] SpendingsCategories { get; set; }

        public DashboardViewModel(IUnitOfWork unitOfWork, IPeriodModel periodModel)
        {
            ImportantIncomes = new ObservableCollection<TransactionModel>();
            ImportantSpendings = new ObservableCollection<TransactionModel>();

            IncomeCategories = new string[] { 
                "VAST: 50%", 
                "VLOEIBAAR: 30%", 
                "OVERIG: 20%" 
            };
            SpendingsCategories = new string[] 
            { 
                "OVERIG: 25%", 
                "VLOEIBAAR: 45%", 
                "VAST: 30%" 
            };

            _unitOfWork = unitOfWork;
            _periodModel = periodModel;

            NextPeriodCommand = new RelayCommand(GoToNextPeriod);
            PreviousPeriodCommand = new RelayCommand(GoToPreviousPeriod);

            InitTransactions();
            InitSpendings();
        }

        private void GoToPreviousPeriod()
        {
            // Period.Previous();
        }

        private void GoToNextPeriod()
        {
            // Period.Next();
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
