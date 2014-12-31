using CashLight_App.Repositories.Interfaces;
using CashLight_App.DTOs;
using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CashLight_App.Repositories
{
    class PeriodRepository : IPeriodRepository
    {
        private ITransactionRepository _transactionRepository;

        public PeriodRepository(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public Period GetByDate(DateTime date)
        {
            Period period = new Period();

            this.SetDates(ref period, date);
            this.SetTransactions(ref period);
            this.SetImportantTransactions(ref period);
            //this.SetCategories();

            return period;
        }

        private void SetTransactions(ref Period period)
        {
            period.Transactions = _transactionRepository.GetAllBetweenDates(period.StartDate, period.EndDate);
        }

        private void SetImportantTransactions(ref Period period)
        {
            period.ImportantIncomes = _transactionRepository.GetHighestBetweenDates(Enums.InOut.In, 4, period.StartDate, period.EndDate);
            period.ImportantSpendings = _transactionRepository.GetHighestBetweenDates(Enums.InOut.Out, 4, period.StartDate, period.EndDate);
        }

        /// <summary>
        /// Sets Start- and EndDate for passed period
        /// </summary>
        /// <param name="d"></param>
        /// <param name="forward"></param>
        private void SetDates(ref Period period, DateTime date, bool forward = false)
        {
            PeriodDTO i = GetConsistentIncome();

            Transaction firstIncomeBeforeDate = _transactionRepository.GetFirstIncomeBeforeDate(date, i.Account);
            Transaction firstIncomeAfterDate = _transactionRepository.GetFirstIncomeAfterDate(date, i.Account);

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

        private PeriodDTO GetConsistentIncome()
        {
            var name = "NA";
            var account = "NA";
            double averagedeviation = default(double);
            double averageperiod = 31;

            //try
            //{
            //    name = _unitOfWork.Setting.Find(q => q.Key == "Name").OrderByDescending(q => q.Date).FirstOrDefault().Value;
            //    account = _unitOfWork.Setting.Find(q => q.Key == "Account").OrderByDescending(q => q.Date).FirstOrDefault().Value;
            //    averagedeviation = Convert.ToDouble(_unitOfWork.Setting.Find(q => q.Key == "AverageDeviation").OrderByDescending(q => q.Date).FirstOrDefault().Value);
            //    averageperiod = Convert.ToDouble(_unitOfWork.Setting.Find(q => q.Key == "AveragePeriod").OrderByDescending(q => q.Date).FirstOrDefault().Value);
            //}
            //catch (Exception)
            //{

            //}

            return new PeriodDTO(name, account, averagedeviation, averageperiod);
        }

    }
}
