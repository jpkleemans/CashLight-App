using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models.Interface
{
    public interface IPeriod
    {
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }

        List<ITransaction> Transactions { get; set; }
        List<ITransaction> ImportantIncomes { get; set; }
        List<ITransaction> ImportantSpendings { get; set; }

        List<ICategory> Categories { get; set; }
    }
}
