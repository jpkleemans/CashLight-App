using CashLight.Services.CSV;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLight.Model.Interface
{
    public interface IBank
    {
        Dictionary<string, string> types { get; set; }

        Dictionary<string, string> CsvToDictionary(CsvRow row);


    }
}
