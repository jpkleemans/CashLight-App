using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Services.BankConverter.Banks
{
    public interface IBank
    {
        Dictionary<string, string> RowToDictionary(List<string> row);
    }
}
