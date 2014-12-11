using CashLight_App.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLight_App.Services.CSV
{
    public class CsvFileReader : StreamReader
    {

        public IBank bank { get; set; }

        public void FileNameToStream(string filename)
        {
            Windows.Storage.FileIO.
        }

        public CsvFileReader(IBank bank, Stream stream)
            : base(stream)
        {
            this.bank = bank;

        }

        public CsvFileReader(IBank bank, string filename)
            : base (filename)
        {
            this.bank = bank;
        }

        public List<Dictionary<string, string>> ReadToList()
        {

            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

            while (!EndOfStream)
            {

                CsvRow row = new CsvRow(); // create a new row-class
                this.ReadRow(row); // set a new row (file) in the class

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
        public bool ReadRow(CsvRow row)
        {
            string line = ReadLine();
            //Console.WriteLine(line);

            row.LineText = line;

            //string[] values = line.Split(new string[] { " , " }, StringSplitOptions.None);

            //string[] values = line.Split(',');
            string[] values = line.Trim('"').Split(new String[] { "\",\"" }, StringSplitOptions.None);

            int i = 0;

            foreach (string value in values)
            {
                var testValue = value;
                var testName = "field" + i;
                row.LineList.Add(testName, testValue);
                i++;
            }

            if (String.IsNullOrEmpty(row.LineText))
                return false;

            int pos = 0;
            int rows = 0;

            while (pos < row.LineText.Length)
            {
                string value;

                // Special handling for quoted field
                if (row.LineText[pos] == '"')
                {
                    // Skip initial quote
                    pos++;

                    // Parse quoted value
                    int start = pos;
                    while (pos < row.LineText.Length)
                    {
                        // Test for quote character
                        if (row.LineText[pos] == '"')
                        {
                            // Found one
                            pos++;

                            // If two quotes together, keep one
                            // Otherwise, indicates end of value
                            if (pos >= row.LineText.Length || row.LineText[pos] != '"')
                            {
                                pos--;
                                break;
                            }
                        }
                        pos++;
                    }
                    value = row.LineText.Substring(start, pos - start);
                    value = value.Replace("\"\"", "\"");
                }
                else
                {
                    // Parse unquoted value
                    int start = pos;
                    while (pos < row.LineText.Length && row.LineText[pos] != ',')
                        pos++;
                    value = row.LineText.Substring(start, pos - start);
                }

                // Add field to list
                if (rows < row.Count)
                    row[rows] = value;
                else
                    row.Add(value);
                rows++;

                // Eat up to and including next comma
                while (pos < row.LineText.Length && row.LineText[pos] != ',')
                    pos++;
                if (pos < row.LineText.Length)
                    pos++;
            }
            // Delete any unused items
            while (row.Count > rows)
                row.RemoveAt(rows);

            // Return true if any columns read
            return (row.Count > 0);
        }
    }
}
