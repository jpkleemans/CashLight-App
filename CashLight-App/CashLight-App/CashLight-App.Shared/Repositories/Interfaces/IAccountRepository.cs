using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> FindAll();

        IEnumerable<Account> FindAllCategorized();

        void Add(Account account);

        void Delete(Account account);

        void Commit();
    }
}
