using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Services.CSV.Banks
{
    public class ING : IBank
    {
        public Dictionary<string, string> types
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        int rowTime = 0;

        string[] RQ = new string[9];

        public Dictionary<string, string> CsvToDictionary(Dictionary<string, string> row)
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
                for (int i = 0; i < row.Count; i++)
                {

                    RQ[i] = RemoveQuotations(row["field" + i]);
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

                if (row.Count < 8)
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

        public string RemoveQuotations(string s)
        {
            string replaced = s.Replace("\"", "");
            return replaced;
        }
    }
}
