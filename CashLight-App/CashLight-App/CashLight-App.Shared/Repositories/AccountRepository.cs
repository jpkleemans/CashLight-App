using CashLight_App.Models;
using CashLight_App.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CashLight_App.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private ITransactionRepository _transactionRepo;

        public AccountRepository(ITransactionRepository transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public IEnumerable<Account> FindAllSpending()
        {
            List<Account> accounts = new List<Account>();

            IEnumerable<Transaction> transactions = _transactionRepo.GetAllSpendings();

            foreach (Transaction transaction in transactions)
            {
                Account account = accounts.Where(x => x.Number == transaction.CreditorNumber).FirstOrDefault();

                if (account != null)
                {
                    account.Transactions.Add(transaction);
                    account.TransactionCount++;
                    account.TotalAmount += transaction.Amount;
                }
                else
                {
                    account = new Account();
                    account.Number = transaction.CreditorNumber;
                    account.Name = transaction.CreditorName;

                    account.Transactions = new List<Transaction>();
                    account.Transactions.Add(transaction);
                    account.TransactionCount = 1;
                    account.TotalAmount = transaction.Amount;

                    accounts.Add(account);
                }
            }

            return accounts;
        }
    }
}
