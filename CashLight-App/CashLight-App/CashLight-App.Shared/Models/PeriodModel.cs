using CashLight_App.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using CashLight_App.DataModels;
using CashLight_App.Models.Interfaces;
using System.Collections.ObjectModel;

namespace CashLight_App.Models
{
    public class PeriodModel : ModelBase, IPeriodModel
    {
        public ObservableCollection<TransactionModel> Transactions { get; set; }

        public PeriodModel()
        {
            SetDates(DateTime.Now, false);
        }

        public PeriodModel(DateTime d)
        {
            SetDates(d);
        }


        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void Next()
        {
            DateTime dateInNextPeriod = EndDate.AddDays(1);
            SetDates(dateInNextPeriod);
        }

        public void Previous()
        {
            DateTime dateInPreviousPeriod = StartDate.AddDays(-1);
            SetDates(dateInPreviousPeriod, false);
        }

        public void SetDates(DateTime d, bool forward = true)
        {
            var i = GetConsistentIncome();
            var latestIncome = _unitOfWork.Transaction.FindAll()
                .Where(q => q.Tegenrekening == i.Account)
                .Where(q => q.Datum <= d)
                .OrderByDescending(q => q.Datum)
                .LastOrDefault();

            var firstIncome = _unitOfWork.Transaction.FindAll()
                .Where(q => q.Tegenrekening == i.Account)
                .Where(q => q.Datum > d)
                .OrderBy(q => q.Datum)
                .FirstOrDefault();

            if (latestIncome == null)
            {
                if (firstIncome == null)
                {
                    if (forward)
                    {
                        this.StartDate = d;
                    }
                    else
                    {
                        this.StartDate = d.AddDays(-i.AveragePeriod);
                    }

                    this.EndDate = StartDate.AddDays(i.AveragePeriod);
                }
                else
                {
                    this.EndDate = firstIncome.Datum.AddDays(-1);
                    this.StartDate = EndDate.AddDays(-i.AveragePeriod);
                }
            }
            else
            {
                if (firstIncome == null)
                {
                    this.StartDate = firstIncome.Datum;
                    this.EndDate = StartDate.AddDays(i.AveragePeriod);
                }
                else
                {
                    this.StartDate = latestIncome.Datum;
                    this.EndDate = firstIncome.Datum.AddDays(-1);
                }
            }

            Transactions = new ObservableCollection<TransactionModel>(
                TransactionModel.All()
                    .Where(q => q.Datum >= this.StartDate && q.Datum <= this.EndDate)
                    .OrderBy(q => q.Datum)
            );
        }

        public static void SearchMostConsistentIncome()
        {
            // Get all returning incomes and group them
            var returningIncomeGroups = _unitOfWork.Transaction.FindAll()
                .Where(a => a.AfBij == (int)Enums.AfBij.Bij)
                .GroupBy(b => b.Tegenrekening)
                .Where(c => c.Count() > 1)
                .ToList();

            // Dictionary with information about most consistent income account
            PeriodDTO mostConsistentIncomeAccount = new PeriodDTO();

            // Loop trough all income groups
            double previousAverageDeviation = default(double);
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
                        periods.Add((previousDate - transaction.Datum).TotalDays);
                    }
                    previousDate = transaction.Datum;

                    accountInfo.Name = transaction.Naam;
                    accountInfo.Account = transaction.Tegenrekening;
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
                    if (previousAverageDeviation != default(double))
                    {
                        if (averageDeviation < previousAverageDeviation)
                        {
                            mostConsistentIncomeAccount = accountInfo;
                        }
                    }
                    previousAverageDeviation = averageDeviation;
                }
            }

            // Ceil some averages before return
            mostConsistentIncomeAccount.AverageDeviation = Math.Ceiling(mostConsistentIncomeAccount.AverageDeviation);
            mostConsistentIncomeAccount.AveragePeriod = Math.Ceiling(mostConsistentIncomeAccount.AveragePeriod);

            //Save
            SaveConsistentIncome(mostConsistentIncomeAccount);
        }

        public PeriodDTO GetConsistentIncome()
        {
            var name = "NA";
            var account = "NA";
            double averagedeviation = default(double);
            double averageperiod = 31;

            try
            {
                name = _unitOfWork.Setting.Find(q => q.Key == "Name").OrderByDescending(q => q.Date).FirstOrDefault().Value;
                account = _unitOfWork.Setting.Find(q => q.Key == "Account").OrderByDescending(q => q.Date).FirstOrDefault().Value;
                averagedeviation = Convert.ToDouble(_unitOfWork.Setting.Find(q => q.Key == "AverageDeviation").OrderByDescending(q => q.Date).FirstOrDefault().Value);
                averageperiod = Convert.ToDouble(_unitOfWork.Setting.Find(q => q.Key == "AveragePeriod").OrderByDescending(q => q.Date).FirstOrDefault().Value);
            }
            catch (Exception)
            {

            }

            return new PeriodDTO(name, account, averagedeviation, averageperiod);
        }

        public static void SaveConsistentIncome(PeriodDTO p)
        {
            Setting s = new Setting("Name", p.Name);
            _unitOfWork.Setting.Add(s);

            Setting s1 = new Setting("Account", p.Account);
            _unitOfWork.Setting.Add(s1);

            Setting s2 = new Setting("AverageDeviation", p.AverageDeviation);
            _unitOfWork.Setting.Add(s2);

            Setting s3 = new Setting("AveragePeriod", p.AveragePeriod);
            _unitOfWork.Setting.Add(s3);

            _unitOfWork.Commit();
        }

        /// <summary>
        /// Haalt de belangrijkste inkomsten op uit de database
        /// </summary>
        /// <param name="list">Transacties</param>
        /// <param name="startdate">Startdatum</param>
        /// <param name="enddate">Einddatum</param>
        /// <returns></returns>
        public List<TransactionModel> getMostImportantIncomes()
        {
            List<TransactionModel> transactions = Transactions
                .Where(x => x.AfBij == (int)Enums.AfBij.Bij)
                .OrderBy(x => x.Bedrag)
                .Take(4)
                .OrderBy(x => x.Datum)
                .ToList();

            TransactionModel.SetHeight(ref transactions);

            return transactions;
        }

        /// <summary>
        /// Haalt de belangrijkste uitgaven op uit de database
        /// </summary>
        /// <param name="list">Transacties</param>
        /// <param name="startdate">Startdatum</param>
        /// <param name="enddate">Einddatum</param>
        /// <returns></returns>
        public List<TransactionModel> getMostImportantSpendings()
        {
            List<TransactionModel> transactions = Transactions
           .Where(x => x.AfBij == (int)Enums.AfBij.Af)
           .OrderBy(x => x.Bedrag)
           .Take(4)
           .OrderBy(x => x.Datum)
           .ToList();

            TransactionModel.SetHeight(ref transactions);

            return transactions;
        }
    }
}
