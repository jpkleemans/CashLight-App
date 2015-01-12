using CashLight_App.Repositories.Interfaces;
using CashLight_App.DTOs;
using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CashLight_App.Enums;
using AutoMapper;
using System.Diagnostics;

namespace CashLight_App.Repositories
{
    class PeriodRepository : IPeriodRepository
    {
        private ITransactionRepository _transactionRepo;
        private ISettingRepository _settingRepo;
        private ICategoryRepository _categoryRepo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="transactionRepo"></param>
        /// <param name="settingRepo"></param>
        /// <param name="categoryRepo"></param>
        public PeriodRepository(ITransactionRepository transactionRepo,
                                ISettingRepository settingRepo,
                                ICategoryRepository categoryRepo)
        {
            _transactionRepo = transactionRepo;
            _settingRepo = settingRepo;
            _categoryRepo = categoryRepo;
        }

        /// <summary>
        /// Returns period by a specific date
        /// </summary>
        /// <param name="date"></param>
        /// <returns>Period</returns>
        public Period GetByDate(DateTime date)
        {
            Period period = new Period();

            this.SetDates(ref period, date);
            this.SetTransactions(ref period);
            this.SetImportantTransactions(ref period);
            //this.SetCategories(ref period);
            this.GetSpendingLimit(ref period);

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
        /// <summary>
        /// Sets the transactions to the current period
        /// </summary>
        /// <param name="period"></param>
        private void SetTransactions(ref Period period)
        {
            period.Transactions = _transactionRepo.GetAllBetweenDates(period.StartDate, period.EndDate);
        }

        /// <summary>
        /// Sets the important transactions to the current period
        /// </summary>
        /// <param name="period"></param>
        private void SetImportantTransactions(ref Period period)
        {
            var startdate = period.StartDate;
            var enddate = period.EndDate;
            period.ImportantIncomes = _transactionRepo.GetHighestBetweenDates(Enums.InOut.In, 4, period.StartDate, period.EndDate);

            IEnumerable<Category> category = _categoryRepo.FindAll().OrderByDescending(q => q.Budget).Take(4);
            /*
            IEnumerable<ImportantCategory> importantcategories = from a in category
                                                                 select new ImportantCategory
                                                                 {
                                                                     Category = a,
                                                                     PercentageOfBudget = (int)a.Budget != 0 ? (int)((from b in _transactionRepo.GetAllBetweenDates(startdate, enddate)
                                                                                          where b.InOut == (int)InOut.Out && b.CategoryID == a.CategoryID
                                                                                          select b.Amount).Sum() / a.Budget) * 100 : 0
                                                                 };
            
            */
            List<ImportantCategory> importantcategories = (from a in category
                                                                 select new ImportantCategory
                                                                 {
                                                                     Category = a,
                                                                 }).ToList();
            
            foreach(var item in importantcategories)
            {
                if((int)item.Category.Budget != 0)
                {
                    double total = 0;
                    foreach(var trx in _transactionRepo.GetAllBetweenDates(startdate, enddate).Where(q => q.InOut == (int)InOut.Out && q.CategoryID == item.Category.CategoryID))
                    {
                        total += trx.Amount;
                    }
                    item.AmountOfBudget = total;

                    double calc = total / item.Category.Budget;
                    var percentage = calc * 100;
                    item.PercentageOfBudget = Convert.ToInt16(percentage);
                }
                else
                {
                    item.PercentageOfBudget = 0;
                    item.AmountOfBudget = 0;
                }
            }
             
            period.ImportantSpendingCategories = importantcategories;
             
        }
        
        /// <summary>
        /// This method returs the most consistent imcome
        /// </summary>
        /// <returns>PeriodDTO</returns>
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
                //averagedeviation = Convert.ToDouble(_settingRepo.FindByKey("Income.AverageDeviation").Value);
                //averageperiod = Convert.ToDouble(_settingRepo.FindByKey("Income.AveragePeriod").Value);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }

            return new PeriodDTO(name, account, averagedeviation, averageperiod);
        }

        /// <summary>
        /// This method searches for the most consistent income.
        /// Saves it to the Settings-table
        /// </summary>
        public void SearchMostConsistentIncome()
        {
            // Get all returning incomes and group them
            IEnumerable<IEnumerable<Transaction>> returningIncomeGroups = _transactionRepo.FindAll()
                .Where(a => a.InOut == (int)Enums.InOut.In)
                .GroupBy(b => b.CreditorNumber)
                .Where(c => c.Count() > 1);

            // Dictionary with information about most consistent income account
            PeriodDTO mostConsistentIncomeAccount = new PeriodDTO();

            // Loop trough all income groups
            foreach (var group in returningIncomeGroups)
            {
                // Hashtable with info about current account
                PeriodDTO accountInfo = new PeriodDTO();

                // List with all periods between two transactions of current account
                List<double> periods = new List<double>();

                // Loop trough all transactions
                DateTime previousDate = default(DateTime);
                foreach (Transaction transaction in group)
                {
                    if (previousDate != default(DateTime))
                    {
                        // Add difference between dates (period) to periods list
                        periods.Add((previousDate - transaction.Date).TotalDays);
                    }
                    previousDate = transaction.Date;

                    accountInfo.Name = transaction.CreditorName;
                    accountInfo.Account = transaction.CreditorNumber;
                }

                // If there are at least 2 periods
                if (periods.Count > 1)
                {
                    // Average period between transactions
                    double average = periods.Average();
                    accountInfo.AveragePeriod = average;

                    // Sum deviation from average
                    double deviation = 0;
                    foreach (double item in periods)
                    {
                        deviation += Math.Abs(average - item);
                    }

                    // Average deviation
                    double averageDeviation = (deviation / periods.Count);
                    accountInfo.AverageDeviation = averageDeviation;

                    // Find most consistent income account
                    if (mostConsistentIncomeAccount.AverageDeviation == 0)
                    {
                        mostConsistentIncomeAccount = accountInfo;
                    }
                    else
                    {
                        if (averageDeviation < mostConsistentIncomeAccount.AverageDeviation)
                        {
                            mostConsistentIncomeAccount = accountInfo;
                        }
                    }
                }
            }

            // Ceil some averages before return
            mostConsistentIncomeAccount.AverageDeviation = Math.Ceiling(mostConsistentIncomeAccount.AverageDeviation);
            mostConsistentIncomeAccount.AveragePeriod = Math.Ceiling(mostConsistentIncomeAccount.AveragePeriod);

            //Save
            SaveConsistentIncome(mostConsistentIncomeAccount);
        }

        /// <summary>
        /// Saves the consistent income
        /// </summary>
        /// <param name="p"></param>
        public void SaveConsistentIncome(PeriodDTO p)
        {
            Setting s = new Setting("Income.CreditorName", p.Name);
            _settingRepo.Add(s);

            Setting s1 = new Setting("Income.CreditorNumber", p.Account);
            _settingRepo.Add(s1);

            Setting s2 = new Setting("Income.AverageDeviation", p.AverageDeviation.ToString());
            _settingRepo.Add(s2);

            Setting s3 = new Setting("Income.AveragePeriod", p.AveragePeriod.ToString());
            _settingRepo.Add(s3);

            _settingRepo.Commit();
        }

        /// <summary>
        /// This method gets the spendingsLimit from the given period
        /// </summary>
        /// <param name="period"></param>
        public void GetSpendingLimit(ref Period period)
        {
            double spendinglimit = 0;

            foreach (var transaction in period.Transactions)
            {
                if ((InOut)transaction.InOut == InOut.In)
                {
                    spendinglimit += transaction.Amount;
                }
            }
            var categories = _categoryRepo.FindAll();
            foreach (var category in categories)
                {
                spendinglimit -= category.Budget;
            }

            period.SpendingsLimit = spendinglimit;
        }

        /// <summary>
        /// GetAll-method
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Period> GetAll()
        {
            Transaction firstTransaction = _transactionRepo.GetFirst();

            List<Period> periods = new List<Period>();

            if (firstTransaction != null)
            {
                Period firstPeriod = GetByDate(firstTransaction.Date);
                periods.Add(firstPeriod);

                Period prevPeriod = firstPeriod;

                while (true)
                {
                    Period curPeriod = GetByDate(prevPeriod.EndDate.AddDays(1));

                    if ((curPeriod.Transactions.Count() > 0) && (curPeriod.EndDate != prevPeriod.EndDate))
                    {
                        periods.Add(curPeriod);
                    }
                    else
                    {
                        break;
                    }

                    prevPeriod = curPeriod;
                }
            }

            return periods;
        }
    }
}
