using CashLight_App.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Interface
{
    interface IUnitOfWork
    {
        IRepository<Transaction> Transaction { get; }
        IRepository<Category> Category { get; }

        //Opslaan gegevens
        void Commit();
    }
}
