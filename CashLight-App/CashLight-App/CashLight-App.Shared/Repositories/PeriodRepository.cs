using CashLight_App.Repositories.Interfaces;
using CashLight_App.DTOs;
using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CashLight_App.Enums;

namespace CashLight_App.Repositories
{
    class PeriodRepository : IPeriodRepository
    {
        private ITransactionRepository _transactionRepo;
        private ISettingRepository _settingRepo;
        private ICategoryRepository _categoryRepo;

        public PeriodRepository(ITransactionRepository transactionRepo,
                                ISettingRepository settingRepo,
                                ICategoryRepository categoryRepo)
        {
            _transactionRepo = transactionRepo;
            _settingRepo = settingRepo;
            _categoryRepo = categoryRepo;
        }

        public Period GetByDate(DateTime date)
        {
            Period period = new Period();

            this.SetDates(ref period, date);
            this.SetTransactions(ref period);
            this.SetImportantTransactions(ref period);
            this.SetCategories(ref period);

            return period;
        }

        /// <summary>
        /// Sets Start- and EndDate for passed period
        /// </summary>
        /// <param name="d"></param>
        /// <param name="forward"></param>
        private void SetDates(ref Period period, DateTime date, bool forward = false)
        {
            PeriodDTO i = GetConsistentIncome();

            Transaction firstIncomeBeforeDate = _transactionRepo.GetFirstIncomeBeforeDate(date, i.Account);
            Transaction firstIncomeAfterDate = _transactionRepo.GetFirstIncomeAfterDate(date, i.Account);

            if (firstIncomeBeforeDate == null)
            {
                if (firstIncomeAfterDate == null)
                {
                    if (forward)
                    {
                        period.StartDate = date;
                    }
                    else
                    {
                        period.StartDate = date.AddDays(-i.AveragePeriod);
                    }

                    period.EndDate = period.StartDate.AddDays(i.AveragePeriod);
                }
                else
                {
                    period.EndDate = firstIncomeAfterDate.Date.AddDays(-1);
                    period.StartDate = period.EndDate.AddDays(-i.AveragePeriod);
                }
            }
            else
            {
                if (firstIncomeAfterDate == null)
                {
                    period.StartDate = firstIncomeBeforeDate.Date;
                    period.EndDate = period.StartDate.AddDays(i.AveragePeriod);
                }
                else
                {
                    period.StartDate = firstIncomeBeforeDate.Date;
                    period.EndDate = firstIncomeAfterDate.Date.AddDays(-1);
                }
            }
        }

        private void SetTransactions(ref Period period)
        {
            period.Transactions = _transactionRepo.GetAllBetweenDates(period.StartDate, period.EndDate);
        }

        private void SetImportantTransactions(ref Period period)
        {
            period.ImportantIncomes = _transactionRepo.GetHighestBetweenDates(Enums.InOut.In, 4, period.StartDate, period.EndDate);
            period.ImportantSpendings = _transactionRepo.GetHighestBetweenDates(Enums.InOut.Out, 4, period.StartDate, period.EndDate);
        }

        private void SetCategories(ref Period period)
        {
            IEnumerable<Category> categories = _categoryRepo.FindAll();

            foreach (Category category in categories)
            {
                double totaltransactions = period.Transactions
                    .Where(q => q.CategoryID == category.CategoryID)
                    .Where(q => q.InOut == (int)InOut.Out)
                    .Count();

                double amountoftransactions = period.Transactions.Count();

                if (totaltransactions == 0 || amountoftransactions == 0)
                {
                    category.Percentage = 0;
                }
                else
                {
                    category.Percentage = Convert.ToInt16((amountoftransactions / totaltransactions) * 100);
                }
            }

            period.Categories = categories;
        }

        private PeriodDTO GetConsistentIncome()
        {
            var name = "NA";
            var account = "NA";
            double averagedeviation = default(double);
            double averageperiod = 31;

            try
            {
                name = _settingRepo.FindByKey("Income.CreditorName").Value;
                account = _settingRepo.FindByKey("Income.CreditorNumber").Value;
                averagedeviation = Convert.ToDouble(_settingRepo.FindByKey("Income.AverageDeviation").Value);
                averageperiod = Convert.ToDouble(_settingRepo.FindByKey("Income.AveragePeriod").Value);
            }
            catch (Exception)
            {

            }

            return new PeriodDTO(name, account, averagedeviation, averageperiod);
        }

    }
}
