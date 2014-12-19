using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models.Interface
{
    public interface ITransaction
    {
        DateTime Date { get; set; }
        string DebtorNumber { get; set; }
        string CreditorName { get; set; }
        string CreditorNumber { get; set; }
        int Code { get; set; }
        int InOut { get; set; }
        double Amount { get; set; }
        string Description { get; set; }

        Category Category { get; set; }

        double Height { get; set; }
    }
}
