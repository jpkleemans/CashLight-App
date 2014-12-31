using CashLight_App.Services.Interfaces;
using Microsoft.Practices.ServiceLocation;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Repositories
{
    public class RepositoryBase
    {
        protected SQLiteConnection _context = ServiceLocator.Current.GetInstance<IDatabase>().Context;
    }
}
