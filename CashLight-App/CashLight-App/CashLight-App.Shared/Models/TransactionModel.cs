using AutoMapper;
using CashLight_App.DataModels;
using CashLight_App.Services.Interface;
using CashLight_App.Views.Dashboard;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Windows.UI.Xaml;

namespace CashLight_App.Models
{
    public class TransactionModel : ModelBase
    {
        public double Height { get; set; }

        public static IEnumerable<TransactionModel> All()
        {
            Mapper.CreateMap<Transaction, TransactionModel>();
            var p = _unitOfWork.Transaction.FindAll();
            return Mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionModel>>(p);
        }

        public bool Exists(Dictionary<string, string> item)
        {
            var name = item["Naam / Omschrijving"];
            var rekening = item["Rekening"];
            var datum = Convert.ToDateTime(item["Datum"]);
            var bedrag = Double.Parse(item["Bedrag (EUR)"]);

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

        public static void removeEqualTransactions(ref List<Transaction> list, Transaction account)
        {
            List<Transaction> jopie = new List<Transaction>();
            foreach (Transaction transaction in list)
            {
                if (transaction.Tegenrekening == account.Tegenrekening &&
                    transaction.AfBij == account.AfBij)
                {
                    jopie.Add(transaction);
                }
            }

            foreach (Transaction item in jopie)
            {
                list.Remove(item);
            }

        }

        /// <summary>
        /// Haalt de belangrijkste inkomsten op uit de database
        /// </summary>
        /// <param name="list">Transacties</param>
        /// <param name="startdate">Startdatum</param>
        /// <param name="enddate">Einddatum</param>
        /// <returns></returns>
        public static List<TransactionModel> getMostImportantTransactionsBij(List<TransactionModel> list, DateTime startdate, DateTime enddate)
        {
            IQueryable<TransactionModel> transactions = list.AsQueryable();

            List<TransactionModel> trans = (from a in transactions
                                            where a.AfBij == (int)Enums.AfBij.Bij
                                                // && a.Category != null
                                            && a.Datum > startdate
                                            && a.Datum < enddate
                                            orderby a.Bedrag descending
                                            select a
                    ).Take(4).ToList();

            double highest = 0;
            foreach (var item in trans)
            {
                if (item.Bedrag > highest)
                {
                    highest = item.Bedrag;
                }
            }

            double maxHeight = (Window.Current.Bounds.Height / 2) - 50; //Max height off the markers.
            double minHeight = 230; //Min height off the markers.
            double useableHeight = maxHeight - minHeight;

            foreach (TransactionModel item in trans)
            {
                double percentage = (item.Bedrag / highest);

                item.Height = (useableHeight * percentage) + minHeight;
            }

            return trans;
        }

        /// <summary>
        /// Haalt de belangrijkste uitgaven op uit de database
        /// </summary>
        /// <param name="list">Transacties</param>
        /// <param name="startdate">Startdatum</param>
        /// <param name="enddate">Einddatum</param>
        /// <returns></returns>
        public static List<Transaction> getMostImportantTransactionsAf(ref List<Transaction> list, DateTime startdate, DateTime enddate)
        {
            IQueryable<Transaction> transactions = list.AsQueryable();
            return (from a in transactions
                    where a.AfBij == (int)Enums.AfBij.Af
                        // && a.Category != null
                    && a.Datum > startdate
                    && a.Datum < enddate
                    orderby a.Bedrag descending
                    select a
                    ).Take(5).ToList();
        }

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
    }
}
