using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Services.BankConverter
{
    public interface IBankConverterService
    {
        List<Dictionary<string, string>> Convert(List<List<string>> list);
    }
}
