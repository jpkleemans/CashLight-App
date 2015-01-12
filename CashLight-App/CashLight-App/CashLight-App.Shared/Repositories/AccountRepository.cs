using CashLight_App.Models;
using CashLight_App.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CashLight_App.Tables;
using CashLight_App.Services.SQLite;
using AutoMapper;

namespace CashLight_App.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private ITransactionRepository _transactionRepo;
        private ISQLiteService _db;

        public AccountRepository(ISQLiteService SQLiteService, ITransactionRepository transactionRepo)
        {
            _db = SQLiteService;
            _transactionRepo = transactionRepo;

            Mapper.CreateMap<AccountCategoryTable, Account>();
        }

        /// <summary>
        /// FindAll returns all transactions from the table
        /// </summary>
        /// <returns>IEnumerable<Account></returns>
        public IEnumerable<Account> FindAll()
        {
            List<Account> accounts = new List<Account>();

            IEnumerable<Transaction> transactions = _transactionRepo.GetAllSpendings();
            IEnumerable<Account> categorizedAccounts = FindAllCategorized();

            foreach (Transaction transaction in transactions)
            {
                Account account = accounts.Where(x => x.Number == transaction.CreditorNumber).FirstOrDefault();
                if (account == null)
                {
                    account = new Account();
                    account.Number = transaction.CreditorNumber;
                    account.Name = transaction.CreditorName;

                    account.Transactions = new List<Transaction>();
                    account.Transactions.Add(transaction);
                    account.TransactionCount = 1;
                    account.TransactionTotalAmount = transaction.Amount;

                    accounts.Add(account);
                }
                else
                {
                    account.Transactions.Add(transaction);
                    account.TransactionCount++;
                    account.TransactionTotalAmount += transaction.Amount;
                }

                Account categorizedAccount = categorizedAccounts.Where(x => x.Number == account.Number).FirstOrDefault();
                if (categorizedAccount != null)
                {
                    account.AccountCategoryID = categorizedAccount.AccountCategoryID;
                    account.CategoryID = categorizedAccount.CategoryID;
                }
            }

            return accounts;
        }

        /// <summary>
        /// Returs all categorized transactions
        /// </summary>
        /// <returns>IEnumerable<Account></returns>
        public IEnumerable<Account> FindAllCategorized()
        {
            TableQuery<AccountCategoryTable> accountCategories = _db.Context.Table<AccountCategoryTable>();

            return Mapper.Map<IEnumerable<AccountCategoryTable>, IEnumerable<Account>>(accountCategories);
        }

        /// <summary>
        /// Adds an account to the database
        /// </summary>
        /// <param name="account"></param>
        public void Add(Account account)
        {
            Mapper.CreateMap<Account, AccountCategoryTable>();
            AccountCategoryTable accountTable = Mapper.Map<Account, AccountCategoryTable>(account);

            _db.Context.Table<AccountCategoryTable>().Connection.Insert(accountTable);
        }

        /// <summary>
        /// Deletes an account from the database
        /// </summary>
        /// <param name="account"></param>
        public void Delete(Account account)
        {
            AccountCategoryTable accountCategoryTable = new AccountCategoryTable();
            accountCategoryTable.AccountCategoryID = account.AccountCategoryID;

            _db.Context.Table<AccountCategoryTable>().Connection.Delete(accountCategoryTable);
        }

        /// <summary>
        /// Commit-method;
        /// </summary>
        public void Commit()
        {
            _db.Context.Commit();
        }
    }
}
