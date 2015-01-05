using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        void Edit(Transaction transaction);
        void Add(Transaction transaction);

        void Commit(); // TODO Weet niet of dit netjes is hier?

        Transaction GetFirstIncomeBeforeDate(DateTime date, string account);
        Transaction GetFirstIncomeAfterDate(DateTime date, string account);

        IEnumerable<Transaction> GetAllBetweenDates(DateTime startDate, DateTime endDate);

        IEnumerable<Transaction> GetHighestBetweenDates(Enums.InOut afBij, int limit, DateTime startDate, DateTime endDate);

        IEnumerable<Transaction> FindAll();

        bool Exists(Transaction transaction);

        IEnumerable<Transaction> GetAllSpendings();

        Transaction GetFirst();
    }
}
