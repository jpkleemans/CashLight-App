using CashLight_App.Services.CSV;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Services.Banks
{
    public interface IBank
    {
        Dictionary<string, string> types { get; set; }
        Dictionary<string, string> CsvToDictionary(CsvRow row);
    }
}
