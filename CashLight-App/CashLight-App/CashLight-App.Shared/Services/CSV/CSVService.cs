using CashLight_App.Services.CSV.Banks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;

namespace CashLight_App.Services.CSV
{
    public class CSVService : ICSVService
    {
        private StreamReader _reader;

        public List<Dictionary<string, string>> ReadToList(Stream stream, IBank bank)
        {
            this._reader = new StreamReader(stream);

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            while (!_reader.EndOfStream)
            {
                Dictionary<string, string> row = ReadRow();
                Dictionary<string, string> dict = bank.CsvToDictionary(row);

                // add to full list
                if (dict.Count > 0)
                {
                    list.Add(dict);
                }
            }

            return list;
        }

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private Dictionary<string, string> ReadRow()
        {
            string line = _reader.ReadLine();
            string[] values = line.Trim('"').Split(new String[] { "\",\"" }, StringSplitOptions.None);

            Dictionary<string, string> lineList = new Dictionary<string, string>();

            int i = 0;
            foreach (string value in values)
            {
                lineList.Add("field" + i, value);
                i++;
            }

            return lineList;
        }
    }
}
