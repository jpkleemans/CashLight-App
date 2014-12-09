using CashLight_App.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Services.Interface
{
    public interface IUnitOfWork
    {
        IRepository<Transaction> Transaction { get; }
        IRepository<Category> Category { get; }

        //Opslaan gegevens
        void Commit();
    }
}
