using CashLight_App.Interface;
using CashLight_App.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CashLight_App.ViewModel
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
            ImportantIncomes.Add(new TransactionModel(new DateTime(2014, 10, 6), "Zakgeld", 100));
            ImportantIncomes.Add(new TransactionModel(new DateTime(2014, 10, 14), "Salaris", 200));
            ImportantIncomes.Add(new TransactionModel(new DateTime(2014, 10, 22), "Kinderbijslag", 150));
            ImportantIncomes.Add(new TransactionModel(new DateTime(2014, 10, 28), "Hansje", 120));
        }
    }
}
