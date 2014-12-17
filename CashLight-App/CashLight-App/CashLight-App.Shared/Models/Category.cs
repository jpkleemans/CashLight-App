using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using CashLight_App.Tables;
using CashLight_App.Models.Interfaces;
using CashLight_App.Enums;
namespace CashLight_App.Models
{
    public class Category : ModelBase
    {
        /// <summary>
        /// Berekent welk percentage van alle transacties aan deze categorie gekoppeld zijn.
        /// </summary>
        /// <returns></returns>
        public int getIncomePercentage(IPeriodModel p)
        {
            double totaltransactions = p.getTransactions().Where(q => q.CategoryID == this.CategoryID).Where(q => q.AfBij == (int)AfBij.Bij).Count();
            double amountoftransactions = p.getTransactions().Where(q => q.AfBij == (int)AfBij.Bij).Count();
            if (totaltransactions == 0 || amountoftransactions == 0)
            {
                return 0;
            }
            return Convert.ToInt32((totaltransactions / amountoftransactions) * 100);
        }

        public int getSpendingPercentage(IPeriodModel p)
        {
            double totaltransactions = p.getTransactions().Where(q => q.CategoryID == this.CategoryID).Where(q => q.AfBij == (int)AfBij.Af).Count();
            double amountoftransactions = p.getTransactions().Where(q => q.AfBij == (int)AfBij.Af).Count();
            if (totaltransactions == 0 || amountoftransactions == 0)
            {
                return 0;
            }
            return Convert.ToInt32((totaltransactions / amountoftransactions) * 100);
        }

        public static Category getByName(string name)
        {
            Mapper.CreateMap<CategoryTable, Category>();
            var cat = _unitOfWork.Category.Find(q => q.Naam == name).FirstOrDefault();
            return Mapper.Map<CategoryTable, Category>(cat);
        }

        public static IEnumerable<Category> All()
        {
            Mapper.CreateMap<CategoryTable, Category>();
            var c = _unitOfWork.Category.FindAll();
            return Mapper.Map<IEnumerable<CategoryTable>, IEnumerable<Category>>(c);
        }

        public static IEnumerable<Category> AllWithIncomePercents(IPeriodModel periodModel)
        {
            var all = All();
            foreach (var item in all)
            {
                item.IncomePercentage = item.getIncomePercentage(periodModel);
            }

            return all;
        }

        public static IEnumerable<Category> AllWithSpendingPercents(Period periodModel)
        {
            var all = All();
            foreach (var item in all)
            {
                item.SpendingsPercentage = item.getSpendingPercentage(periodModel);
            }

            return all;
        }

        public static void SetRandomCategories()
        {
            var catlist = Category.All().ToList();
            var transactions = _unitOfWork.Transaction.FindAll().ToList();
            var random = new Random();
            foreach(var t in transactions)
            {
                var catid = catlist[random.Next(0, catlist.Count)].CategoryID;
                t.CategoryID = catid;
                _unitOfWork.Transaction.Edit(t);
            }

            _unitOfWork.Commit();
        }

        public int CategoryID { get; set; }
        public string Naam { get; set; }

        public int IncomePercentage { get; set; }

        public int SpendingsPercentage { get; set; }
    }
}
