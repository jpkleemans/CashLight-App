using CashLight_App.Models;
using CashLight_App.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CashLight_App.Tables;
using CashLight_App.Services.SQLite;

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
        }

        public IEnumerable<Account> FindAllSpending()
        {
            List<Account> accounts = new List<Account>();

            IEnumerable<AccountCategoryTable> alreadyCategorizedAccounts = _db.Context.Table<AccountCategoryTable>();

            IEnumerable<Transaction> transactions = _transactionRepo.GetAllSpendings();

            foreach (Transaction transaction in transactions)
            {
                Account account = accounts.Where(x => x.Number == transaction.CreditorNumber).FirstOrDefault();

                if (account != null)
                {
                    account.Transactions.Add(transaction);
                    account.TransactionCount++;
                    account.TransactionTotalAmount += transaction.Amount;
                }
                else
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

                AccountCategoryTable categorizedAccount = alreadyCategorizedAccounts.Where(x => x.AccountNumber == account.Number).FirstOrDefault();
                if (categorizedAccount != null)
                {
                    account.AccountCategoryID = categorizedAccount.AccountCategoryID;
                    account.CategoryID = categorizedAccount.CategoryID;
                }
                else
                {
                    account.CategoryID = 0;
                }
            }

            return accounts;
        }

        public void Add(Account account)
        {
            AccountCategoryTable accountCategoryTable = new AccountCategoryTable();

            accountCategoryTable.CategoryID = account.CategoryID;
            accountCategoryTable.AccountNumber = account.Number;

            _db.Context.Table<AccountCategoryTable>().Connection.Insert(accountCategoryTable);
        }

        public List<Account> FindAll()
        {
            IEnumerable<AccountCategoryTable> AccountCategories = _db.Context.Table<AccountCategoryTable>();
            List<Account> list = new List<Account>();
            foreach (var account in AccountCategories)
            {
                var henkie = new Account();
                henkie.AccountCategoryID = account.AccountCategoryID;
                henkie.CategoryID = account.CategoryID;
                henkie.Number = account.AccountNumber;
                list.Add(henkie);
            }
            return list;
        }

        public void Delete(Account account)
        {
            AccountCategoryTable accountCategoryTable = new AccountCategoryTable();

            accountCategoryTable.AccountCategoryID = account.AccountCategoryID;

            _db.Context.Table<AccountCategoryTable>().Connection.Delete(accountCategoryTable);
        }


        public void Commit()
        {
            _db.Context.Commit();
        }
    }
}
