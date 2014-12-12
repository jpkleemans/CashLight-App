using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AutoMapper;
using CashLight_App.DataModels;
using CashLight_App.Models.Interfaces;
using CashLight_App.Enums;
namespace CashLight_App.Models
{
    public class CategoryModel : ModelBase
    {

        /// <summary>
        /// Berekent welk percentage van alle transacties aan deze categorie gekoppeld zijn.
        /// </summary>
        /// <returns></returns>
        public int getIncomePercentage(IPeriodModel p)
        {
            double totaltransactions = p.getTransactions().Where(q => q.CategoryID == this.CategoryID).Where(q => q.AfBij == (int)AfBij.Bij).Count();
            double amountoftransactions = p.getTransactions().Count();
            if(totaltransactions == 0 || amountoftransactions == 0)
            {
                return 0;
            }
            return Convert.ToInt16((amountoftransactions / totaltransactions) * 100);
        }

        public int getSpendingPercentage(IPeriodModel p)
        {
            double totaltransactions = p.getTransactions().Where(q => q.CategoryID == this.CategoryID).Where(q => q.AfBij == (int)AfBij.Af).Count();
            double amountoftransactions = p.getTransactions().Count();
            if (totaltransactions == 0 || amountoftransactions == 0)
            {
                return 0;
            }
            return Convert.ToInt16((amountoftransactions / totaltransactions) * 100);
        }

        public static CategoryModel getByName(string name)
        {
            Mapper.CreateMap<Category, CategoryModel>();
            var cat = _unitOfWork.Category.Find(q => q.Naam == name).FirstOrDefault();
            return Mapper.Map<Category, CategoryModel>(cat);
        }

        public static IEnumerable<CategoryModel> All()
        {
            Mapper.CreateMap<Category, CategoryModel>();
            var c = _unitOfWork.Category.FindAll();
            return Mapper.Map<IEnumerable<Category>, IEnumerable<CategoryModel>>(c);
        }


        public int CategoryID { get; set; }
        public string Naam { get; set; }

        
    }
}
