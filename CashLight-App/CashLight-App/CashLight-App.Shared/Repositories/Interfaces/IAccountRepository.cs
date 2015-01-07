using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        IEnumerable<Account> FindAllSpending();
    }
}
