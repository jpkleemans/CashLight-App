using CashLight_App.Services.BankConverter.Banks;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Services.BankConverter
{
    class BankConverterService : IBankConverterService
    {
        public List<Dictionary<string, string>> Convert(List<List<string>> list)
        {
            IBank bank = new ING(); // TODO: Recognize bank based on CSV format.

            List<Dictionary<string, string>> dict = new List<Dictionary<string, string>>();

            foreach (List<string> row in list)
            {
                var bankRow = bank.RowToDictionary(row);

                if (bankRow.Count > 0)
                {
                    dict.Add(bankRow);
                }
            }

            return dict;
        }
    }
}
