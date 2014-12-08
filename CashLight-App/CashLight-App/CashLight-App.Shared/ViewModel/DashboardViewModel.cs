using CashLight_App.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;

namespace CashLight_App.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        public ObservableCollection<Transaction> ImportantIncomes { get; set; }

        public DashboardViewModel()
        {
            ImportantIncomes = new ObservableCollection<Transaction>();

            InitTransactions();
        }

        public void InitTransactions()
        {
            ImportantIncomes.Add(new Transaction(new DateTime(2014, 10, 6), "Zakgeld", 100));
            ImportantIncomes.Add(new Transaction(new DateTime(2014, 10, 14), "Salaris", 200));
            ImportantIncomes.Add(new Transaction(new DateTime(2014, 10, 22), "Kinderbijslag", 150));
            ImportantIncomes.Add(new Transaction(new DateTime(2014, 10, 28), "Hansje", 120));
        }
    }
}
