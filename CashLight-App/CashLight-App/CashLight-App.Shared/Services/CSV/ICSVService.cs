using CashLight_App.Services.CSV.Banks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CashLight_App.Services.CSV
{
    public interface ICSVService
    {
        List<Dictionary<string, string>> ReadToList(Stream stream, IBank bank);
    }
}
