using CashLight.Model.Interface;
using CashLight.Utility.CSV;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLight.Model
{
    public class ING : IBank
    {
        int rowTime = 0;

        string[] RQ = new string[9];

        // Dictionary implemented from interface IBank
        public Dictionary<string, string> types { get; set; }

        public Dictionary<string, string> CsvToDictionary(CsvRow row)
        {
            // Define a new Dictionary to store the database values
            Dictionary<string, string> database = new Dictionary<string, string>();

            // Skip the first row of the CSV-file
            if (rowTime == 0)
            {
                rowTime++;
            }
            else 
            {
                // Remove the quotations in a string 
                for (int i = 0; i < row.LineList.Count; i++)
                {
                    
                    RQ[i] = RemoveQuotations(row.LineList["field" + i]);
                }

                // Convert date in database to integer
                int year = Convert.ToInt32(RQ[0].Substring(0, 4));
                int month = Convert.ToInt32(RQ[0].Substring(4, 2));
                int day = Convert.ToInt32(RQ[0].Substring(6, 2));

                DateTime datum = new DateTime(year, month, day);
                RQ[0] = datum.ToString();

                // Add the keys and values to the new Dictionary (database)
                database.Add("Datum", RQ[0]);
                database.Add("Naam / Omschrijving", RQ[1]);
                database.Add("Rekening", RQ[2]);
                database.Add("Tegenrekening", RQ[3]);
                database.Add("Code", RQ[4]);
                database.Add("Af / Bij", RQ[5]);
                database.Add("Bedrag (EUR)", RQ[6]);
                database.Add("Mutatiesoort", RQ[7]);

                if (row.LineList.Count < 8)
                {
                    database.Add("Mededelingen", string.Empty);                

                }

                else
                {
                    database.Add("Mededelingen", RQ[8]);                

                }
            }

            return database;
        }

        // Method to replace the quotes stored in a string in the CSV file
        public string RemoveQuotations(string s)
        {
            string replaced = s.Replace("\"", "");
            return replaced;
        }
    }
}   

