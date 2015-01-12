using CashLight_App.Config;
using CashLight_App.Enums;
using CashLight_App.Repositories.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashLight_App.Tables;
using System.Linq;
using CashLight_App.Models;
namespace CashLight_Test.Windows
{
    [TestClass]
    public class AccountRepository
    {

        public IAccountRepository _repo { get; set; }
        public ICategoryRepository _catrepo { get; set; }
        public ITransactionRepository _trxrepo { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Bootstrapper.Initialize(Mode.Testing);
            _repo = ServiceLocator.Current.GetInstance<IAccountRepository>();
            _catrepo = ServiceLocator.Current.GetInstance<ICategoryRepository>();
            _trxrepo = ServiceLocator.Current.GetInstance<ITransactionRepository>();
            Seed();
        }

        public void Seed()
        {
            foreach(var item in _repo.FindAll())
            {
                _repo.Delete(item);
            }
            _repo.Commit();

            foreach (var item in _catrepo.FindAll())
            {
                _catrepo.Delete(item);
            }
            _catrepo.Commit();

            List<Category> categories = new List<Category>()
            {
                new Category { CategoryID = 1, Name = "cat1", Budget = 100, Type = (int)CategoryType.Variable},
                new Category { CategoryID = 2, Name = "cat2", Budget = 200, Type = (int)CategoryType.Variable}
            };
            foreach(var item in categories)
            {
                _catrepo.Add(item);
            }
            _catrepo.Commit();
            List<Account> list = new List<Account>()
            {
                new Account { AccountCategoryID = 1, CategoryID = 1, Name = "test1", Number="1231231" },
                new Account { AccountCategoryID = 2, CategoryID = 2, Name = "test2", Number="1231232" },
                new Account { AccountCategoryID = 3, CategoryID = 1, Name = "test3", Number="1231233" },
                new Account { AccountCategoryID = 4, CategoryID = 2, Name = "test4", Number="1231234" },
                new Account { AccountCategoryID = 5, CategoryID = 1, Name = "test5", Number="1231235" },
            };

            foreach(var item in list)
            {
                _repo.Add(item);
            }

            _repo.Commit();
        }
        [TestMethod]
        public void TestAddAccount()
        {
            var random = new Random();
            var r1 = random.Next();
            var acc = new Account { AccountCategoryID = 1, CategoryID = 1, Name = r1.ToString(), Number = "1231231" };
               _repo.Add(acc);
            var result = _repo.FindAllCategorized().Where(q => q.Name == r1.ToString());
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void TestFindAllAccount()
        {
            var r = new Random();
            var random1 = r.Next();
            var random2 = r.Next();
            var account = new Account { CategoryID = 1, Name = random1.ToString(), Number = random2.ToString() };
            _repo.Add(account);
            _repo.Commit();

            var transaction = new Transaction { CategoryID = 1, CreditorName = "fdsf", Amount = 5, Code = 0, Date = DateTime.Now, CreditorNumber = random2.ToString(), DebtorNumber = string.Empty, Description = string.Empty, InOut = (int)InOut.Out };
            _trxrepo.Add(transaction);
            _trxrepo.Commit();

            var transactions = _trxrepo.GetAllSpendings();
            var gfdsgfsd = _trxrepo.FindAll();
            var result = _repo.FindAll().Where(q => q.Number == random2.ToString()).ToList();
            Assert.AreEqual(1, result.Count());
        }


        [TestMethod]
        public void TestFindAllCategorizedAccount()
        {

            var result = _repo.FindAllCategorized();
            Assert.IsTrue(result.Count() >= 5);
        }

        [TestMethod]
        public void TestDeleteAccount()
        {
            var random = new Random();
            var r1 = random.Next(543543);
            var acc = new Account { AccountCategoryID = r1, CategoryID = 1, Name = string.Empty, Number = "1231231" };
            _repo.Add(acc);
            _repo.Commit();
            _repo.Delete(acc);
            _repo.Commit();
            var result = _repo.FindAllCategorized().Where(q => q.AccountCategoryID == r1);
            Assert.AreEqual(0, result.Count());
        }



    }
}
