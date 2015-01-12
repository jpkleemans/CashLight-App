using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using CashLight_App.Models;
using CashLight_App.Repositories.Interfaces;
using CashLight_App.Tables;
using AutoMapper;
using CashLight_App.Services.SQLite;
using CashLight_App.Enums;

namespace CashLight_App.Repositories
{
    class TransactionRepository : ITransactionRepository
    {
        private ISQLiteService _db;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="SQLiteService"></param>
        public TransactionRepository(ISQLiteService SQLiteService)
        {
            this._db = SQLiteService;

            Mapper.CreateMap<TransactionTable, Transaction>();
        }

        /// <summary>
        /// Edits a specific Transaction
        /// </summary>
        /// <param name="transaction"></param>
        public void Edit(Transaction transaction)
        {
            Mapper.CreateMap<Transaction, TransactionTable>();
            TransactionTable transactionTable = Mapper.Map<Transaction, TransactionTable>(transaction);

            _db.Context.Table<TransactionTable>().Connection.Update(transactionTable);
        
        }

        /// <summary>
        /// Adds a new Transaction
        /// </summary>
        /// <param name="transaction"></param>
        public void Add(Transaction transaction)
        {
            Mapper.CreateMap<Transaction, TransactionTable>();
            TransactionTable transactionTable = Mapper.Map<Transaction, TransactionTable>(transaction);

            _db.Context.Table<TransactionTable>().Connection.Insert(transactionTable);
        }

        /// <summary>
        /// Commit-method
        /// </summary>
        public void Commit()
        {
            _db.Context.Commit();
        }

        /// <summary>
        /// Get first income before a specific Date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public Transaction GetFirstIncomeBeforeDate(DateTime date, string account)
        {
            TableQuery<TransactionTable> transactions = _db.Context.Table<TransactionTable>();

            TransactionTable transaction = transactions
                .Where(q => q.CreditorNumber == account)
                .Where(q => q.Date <= date)
                .OrderBy(q => q.Date)
                .LastOrDefault();

            return Mapper.Map<TransactionTable, Transaction>(transaction);
        }

        /// <summary>
        /// Get first income after a specific Date
        /// </summary>
        /// <param name="date"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public Transaction GetFirstIncomeAfterDate(DateTime date, string account)
        {
            TableQuery<TransactionTable> transactions = _db.Context.Table<TransactionTable>();

            TransactionTable transaction = transactions
                .Where(q => q.CreditorNumber == account)
                .Where(q => q.Date > date)
                .OrderBy(q => q.Date)
                .FirstOrDefault();

            return Mapper.Map<TransactionTable, Transaction>(transaction);
        }

        /// <summary>
        /// Get all income between two dates
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<Transaction> GetAllBetweenDates(DateTime startDate, DateTime endDate)
        {
            TableQuery<TransactionTable> transactions = _db.Context.Table<TransactionTable>();

            IEnumerable<TransactionTable> transactionList = transactions
                    .Where(q => q.Date >= startDate && q.Date <= endDate)
                    .OrderBy(q => q.Date);

            IEnumerable<Transaction> transactionModels = Mapper.Map<IEnumerable<TransactionTable>, IEnumerable<Transaction>>(transactionList);

            IEnumerable<AccountCategoryTable> AccountCategories = _db.Context.Table<AccountCategoryTable>();

            foreach (Transaction transaction in transactionModels)
            {
                AccountCategoryTable account = AccountCategories.Where(x => x.Number == transaction.CreditorNumber).FirstOrDefault();
                if (account != null)
                {
                    transaction.CategoryID = account.CategoryID;
                }
            }

            return transactionModels;
        }

        /// <summary>
        /// Gets the highest Transactions between dates
        /// </summary>
        /// <param name="inOut"></param>
        /// <param name="limit"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IEnumerable<Transaction> GetHighestBetweenDates(Enums.InOut inOut, int limit, DateTime startDate, DateTime endDate)
        {
            TableQuery<TransactionTable> transactions = _db.Context.Table<TransactionTable>();

            IEnumerable<TransactionTable> transactionList = (transactions
                .Where(x => x.InOut == (int)inOut)
                .Where(q => q.Date >= startDate && q.Date <= endDate)
                .OrderBy(x => x.Amount)
                .Take(limit));

            return Mapper.Map<IEnumerable<TransactionTable>, IEnumerable<Transaction>>(transactionList.OrderBy(x => x.Date));
        }

        /// <summary>
        /// Checks if a transaction exists
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public bool Exists(Transaction transaction)
        {
            TableQuery<TransactionTable> transactions = _db.Context.Table<TransactionTable>();

            int count = transactions
                .Where(x => x.CreditorName == transaction.CreditorName)
                .Where(x => x.CreditorNumber == transaction.CreditorNumber)
                .Where(x => x.Date == transaction.Date)
                .Where(x => x.Amount == transaction.Amount)
                .Count();

            if (count == 0)
            {
                return false;
            }

            return true;
        }


        public IEnumerable<Transaction> FindAll()
        {
            TableQuery<TransactionTable> transactions = _db.Context.Table<TransactionTable>();

            return Mapper.Map<IEnumerable<TransactionTable>, IEnumerable<Transaction>>(transactions);
        }


        public IEnumerable<Transaction> GetAllSpendings()
        {
            TableQuery<TransactionTable> transactions = _db.Context.Table<TransactionTable>();

            IEnumerable<TransactionTable> spendings = transactions
                .Where(x => x.InOut == (int)InOut.Out);

            return Mapper.Map<IEnumerable<TransactionTable>, IEnumerable<Transaction>>(spendings);
        }


        public Transaction GetFirst()
        {
            TableQuery<TransactionTable> transactions = _db.Context.Table<TransactionTable>();

            TransactionTable transaction = transactions.LastOrDefault();

            return Mapper.Map<TransactionTable, Transaction>(transaction);
        }
        public void Delete(Transaction transaction)
        {
            TransactionTable TransactionTable = new TransactionTable();
            TransactionTable.TransactionID = transaction.TransactionID;

            _db.Context.Table<TransactionTable>().Connection.Delete(TransactionTable);
        }
    }
}
