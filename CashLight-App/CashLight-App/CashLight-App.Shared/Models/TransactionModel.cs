using CashLight_App.DataModels;
using CashLight_App.Services.Interface;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CashLight_App.Models
{
    public class TransactionModel
    {
        private static int pixels = 500; //Max height off the markers.
        private static IUnitOfWork _unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>();

        public static int height { get; set; }

        public static List<Transaction> GetAll()
        {
            return _unitOfWork.Transaction.FindAll().ToList();
        }

        public void setCategory(Category c)
        {

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
        public static List<Transaction> getMostImportantTransactionsBij(List<Transaction> list, DateTime startdate, DateTime enddate)
        {
            IQueryable<Transaction> transactions = list.AsQueryable();
            List<Transaction> trans = (from a in transactions
                    where a.AfBij == (int)Enums.AfBij.Bij
                   // && a.Category != null
                    && a.Datum > startdate
                    && a.Datum < enddate
                    orderby a.Bedrag descending
                    select a
                    ).Take(4).ToList();
            double total = 0;
            foreach (Transaction item in trans)
            {
                total += item.Bedrag;
            }
            foreach (Transaction item in trans)
            {
                double percentage = (item.Bedrag / total);
                height = pixels * Convert.ToInt32(percentage);
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

    }
}
