using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CashLight_App.Services.CSVReader
{
    public interface ICSVReaderService
    {
        List<List<string>> ReadToList(Stream stream);
    }
}
