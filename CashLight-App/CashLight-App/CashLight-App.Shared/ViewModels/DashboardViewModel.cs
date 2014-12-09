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

        public ObservableCollection<Transaction> ImportantIncomes { get; set; }

        public DashboardViewModel(IUnitOfWork unitOfWork)
        {
            ImportantIncomes = new ObservableCollection<Transaction>();

            _unitOfWork = unitOfWork;

            InitTransactions();
        }

        public void InitTransactions()
        {
            List<Transaction> all = TransactionModel.GetAll();
            List<Transaction> mostImportantIncomes = TransactionModel.getMostImportantTransactionsBij(all, new DateTime(2014, 10, 01), new DateTime(2014, 11, 01));

            foreach (Transaction item in mostImportantIncomes)
            {
                ImportantIncomes.Add(item);
            }
        }
    }
}
