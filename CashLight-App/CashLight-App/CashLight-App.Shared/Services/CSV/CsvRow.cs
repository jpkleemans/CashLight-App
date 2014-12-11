using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLight.Utility.CSV
{
    public class CsvRow : List<string>
    {
        public string LineText { get; set; }

        public Dictionary<string, string> LineList { get; set; }

        public CsvRow()
        {
            this.LineList = new Dictionary<string, string>();
        }

    }
}
