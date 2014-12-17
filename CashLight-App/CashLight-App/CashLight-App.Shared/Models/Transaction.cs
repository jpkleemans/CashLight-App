using AutoMapper;
using CashLight_App.Tables;
using CashLight_App.Services.Interface;
using CashLight_App.Views.Dashboard;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;

namespace CashLight_App.Models
{
    public class Transaction : ModelBase
    {
        public int TransactionID { get; set; }
        public DateTime Datum { get; set; }
        public string Naam { get; set; }
        public string Rekening { get; set; }
        public string Tegenrekening { get; set; }
        public int Code { get; set; }
        public int AfBij { get; set; }
        public double Bedrag { get; set; }
        public string Mededelingen { get; set; }
        public int CategoryID { get; set; }
        public double Height { get; set; }

        public static IEnumerable<Transaction> All()
        {
            Mapper.CreateMap<TransactionTable, Transaction>();
            var p = _unitOfWork.Transaction.FindAll();
            return Mapper.Map<IEnumerable<TransactionTable>, IEnumerable<Transaction>>(p);
        }

        public bool Exists(Dictionary<string, string> item)
        {
            var name = item["Naam / Omschrijving"];
            var rekening = item["Rekening"];
            var datum = Convert.ToDateTime(item["Datum"]);
            var bedrag = Double.Parse(item["Bedrag (EUR)"], new CultureInfo("nl-NL"));

            var list = _unitOfWork.Transaction.FindAll()
                .Where(x => x.Naam == name)
                .Where(x => x.Rekening == rekening)
                .Where(x => x.Datum == datum)
                .Where(x => x.Bedrag == bedrag)
                .ToList();

            if (list.Count == 0)
            {
                return false;
            }

            return true;
        }

        public static List<Transaction> SetHeight(ref List<Transaction> transactions)
        {
            double highest = 0;
            foreach (var item in transactions)
            {
                if (item.Bedrag > highest)
                {
                    highest = item.Bedrag;
                }
            }

            double maxHeight = 1080;

            if (Window.Current != null)
            {
                maxHeight = (Window.Current.Bounds.Height / 2) - 50; //Max height off the markers.
            }

            double minHeight = 230; //Min height off the markers.
            double useableHeight = maxHeight - minHeight;

            foreach (Transaction item in transactions)
            {
                double percentage = (item.Bedrag / highest);

                item.Height = (useableHeight * percentage) + minHeight;
            }

            return transactions;
        }
    }
}
