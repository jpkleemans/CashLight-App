using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Windows.Storage;

namespace CashLight_App.Services.CSVReader
{
    public class CSVReaderService : ICSVReaderService
    {
        private StreamReader _reader;

        public List<List<string>> ReadToList(Stream stream)
        {
            this._reader = new StreamReader(stream);

            List<List<string>> list = new List<List<string>>();

            while (!_reader.EndOfStream)
            {
                List<string> row = ReadRow();

                // add to full list
                if (row.Count > 0)
                {
                    list.Add(row);
                }
            }

            return list;
        }

        /// <summary>
        /// Reads a row of data from a CSV file
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private List<string> ReadRow()
        {
            string line = _reader.ReadLine();
            string[] values = line.Trim('"').Split(new String[] { "\",\"" }, StringSplitOptions.None);

            List<string> lineList = new List<string>();

            foreach (string value in values)
            {
                lineList.Add(value);
            }

            return lineList;
        }
    }
}
