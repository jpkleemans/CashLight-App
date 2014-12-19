using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Services.Interfaces
{
    interface IDatabase
    {
        SQLiteConnection Context { get; set; }
    }
}
