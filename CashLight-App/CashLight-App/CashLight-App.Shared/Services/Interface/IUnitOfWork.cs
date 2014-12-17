using CashLight_App.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Services.Interface
{
    public interface IUnitOfWork
    {
        IRepository<TransactionTable> Transaction { get; }
        IRepository<CategoryTable> Category { get; }

        IRepository<SettingTable> Setting { get; }

        //Opslaan gegevens
        void Commit();
    }
}
