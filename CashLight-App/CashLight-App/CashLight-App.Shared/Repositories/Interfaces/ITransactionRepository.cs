using CashLight_App.Models;
using CashLight_App.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        void Add(ITransaction transaction);

        void Commit(); // TODO Weet niet of dit netjes is hier?

        ITransaction GetFirstIncomeBeforeDate(DateTime date, string account);
        ITransaction GetFirstIncomeAfterDate(DateTime date, string account);

        IEnumerable<ITransaction> GetAllBetweenDates(DateTime startDate, DateTime endDate);

        IEnumerable<ITransaction> GetHighestBetweenDates(Enums.InOut afBij, int limit, DateTime startDate, DateTime endDate);

        IEnumerable<ITransaction> FindAll();

        bool Exists(ITransaction transaction);
    }
}
