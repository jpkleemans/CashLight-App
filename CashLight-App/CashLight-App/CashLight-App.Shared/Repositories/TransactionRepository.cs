using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using CashLight_App.Models;
using CashLight_App.Repositories.Interfaces;
using SQLite;
using CashLight_App.Tables;
using AutoMapper;

namespace CashLight_App.Repositories
{
    class TransactionRepository : RepositoryBase, ITransactionRepository
    {
        public TransactionRepository()
        {
            Mapper.CreateMap<TransactionTable, Transaction>();
        }

        public void Add(Transaction transaction)
        {
            Mapper.CreateMap<Transaction, TransactionTable>();
            TransactionTable transactionTable = Mapper.Map<Transaction, TransactionTable>(transaction);

            base._context.Table<TransactionTable>().Connection.Insert(transactionTable);
        }

        public void Commit()
        {
            base._context.Commit();
        }

        public Transaction GetFirstIncomeBeforeDate(DateTime date, string account)
        {
            TableQuery<TransactionTable> transactions = base._context.Table<TransactionTable>();

            TransactionTable transaction = transactions
                .Where(q => q.CreditorNumber == account)
                .Where(q => q.Date <= date)
                .OrderByDescending(q => q.Date)
                .LastOrDefault();

            return Mapper.Map<TransactionTable, Transaction>(transaction);
        }

        public Transaction GetFirstIncomeAfterDate(DateTime date, string account)
        {
            TableQuery<TransactionTable> transactions = base._context.Table<TransactionTable>();

            TransactionTable transaction = transactions
                .Where(q => q.CreditorNumber == account)
                .Where(q => q.Date > date)
                .OrderBy(q => q.Date)
                .FirstOrDefault();

            return Mapper.Map<TransactionTable, Transaction>(transaction);
        }

        public IEnumerable<Transaction> GetAllBetweenDates(DateTime startDate, DateTime endDate)
        {
            TableQuery<TransactionTable> transactions = base._context.Table<TransactionTable>();

            IEnumerable<TransactionTable> transactionList = transactions
                    .Where(q => q.Date >= startDate && q.Date <= endDate)
                    .OrderBy(q => q.Date);

            return Mapper.Map<IEnumerable<TransactionTable>, IEnumerable<Transaction>>(transactionList);
        }

        public IEnumerable<Transaction> GetHighestBetweenDates(Enums.InOut inOut, int limit, DateTime startDate, DateTime endDate)
        {
            TableQuery<TransactionTable> transactions = base._context.Table<TransactionTable>();

            IEnumerable<TransactionTable> transactionList = transactions
                .Where(x => x.InOut == (int)inOut)
                .Where(q => q.Date >= startDate && q.Date <= endDate)
                .OrderBy(x => x.Amount)
                .Take(limit)
                .OrderBy(x => x.Date);

            // SetHeight not implemented yet

            return Mapper.Map<IEnumerable<TransactionTable>, IEnumerable<Transaction>>(transactionList);
        }

        public bool Exists(Transaction transaction)
        {
            TableQuery<TransactionTable> transactions = base._context.Table<TransactionTable>();

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
            TableQuery<TransactionTable> transactions = base._context.Table<TransactionTable>();

            return Mapper.Map<IEnumerable<TransactionTable>, IEnumerable<Transaction>>(transactions);
        }
    }
}
