using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models.Interfaces
{
    public interface IPeriodModel
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }

        IPeriodModel Next();
        IPeriodModel Previous();
        List<Transaction> getMostImportantIncomes();
        List<Transaction> getMostImportantSpendings();
        IEnumerable<Transaction> getTransactions();
    }
}
