using CashLight_App.Config;
using CashLight_App.Enums;
using CashLight_App.Models;
using CashLight_App.Enums;
using CashLight_App.Repositories.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashLight_Test.Windows
{
[TestClass]
    public class TransactionRepository
    {
        public ITransactionRepository _repo { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Bootstrapper.Initialize(Mode.Testing);
            _repo = ServiceLocator.Current.GetInstance<ITransactionRepository>();

        }
        [TestMethod]
        public void TestAddTransaction()
        {
            var count = _repo.FindAll();
            DateTime now = DateTime.Now;
            Assert.AreEqual(0, count.Count());

            Transaction transaction = new Transaction();
            transaction.Amount = 20;
            transaction.CategoryID = 0;
            transaction.Code = 2;
            transaction.CreditorName = "CreditorName";
            transaction.CreditorNumber = "123456789";
            transaction.Date = now;
            transaction.DebtorNumber = "987654321";
            transaction.Description = "Description";
            transaction.InOut = (int)InOut.In;

            _repo.Add(transaction);

            count = _repo.FindAll();
            Transaction result = _repo.GetFirst();
            //Check if row is added.
            Assert.AreEqual(1, count.Count());
            //Check basic values
            Assert.AreEqual("CreditorName", result.CreditorName);
            

        }
        [TestMethod]
        public void TestEditTransaction()
        {

            var transaction = _repo.GetFirst();

            Assert.AreEqual("CreditorName", transaction.CreditorName,"Name is correct!");            

            transaction.CreditorName = "Henk";

            _repo.Edit(transaction);

            var modified = _repo.GetFirst();
            Assert.AreEqual("Henk", modified.CreditorName);

        }
        [TestMethod]
        public void TestFindAllTransaction()
        {
            this.RemoveAll();

            var count = _repo.FindAll();
            Assert.AreEqual(0, count.Count());

            Transaction transaction_1 = new Transaction();
            transaction_1.CreditorName = "Henk";
            transaction_1.CreditorNumber = "555";
            _repo.Add(transaction_1);

            Transaction transaction_2 = new Transaction();
            transaction_2.CreditorName = "Piet";
            transaction_2.CreditorNumber = "666";
            _repo.Add(transaction_2);

            var all = _repo.FindAll().ToList();

            Assert.AreEqual(2, all.Count());
            Assert.AreEqual("Henk", all[0].CreditorName);
            Assert.AreEqual("Piet", all[1].CreditorName);

            Assert.AreEqual("555", all[0].CreditorNumber);
            Assert.AreEqual("666", all[1].CreditorNumber);

        }
        [TestMethod]
        public void TestExists()
        {
            this.RemoveAll();
            var count = _repo.FindAll();
            Assert.AreEqual(0, count.Count());

            //Added to DB.
            Transaction transaction_1 = new Transaction();
            transaction_1.CreditorName = "Henk";
            transaction_1.CreditorNumber = "555";
            _repo.Add(transaction_1);

            //Did not add to DB.
            Transaction transaction_2 = new Transaction();
            transaction_2.CreditorName = "Piet";
            transaction_2.CreditorNumber = "666";

            Assert.AreEqual(_repo.Exists(transaction_1), true);
            Assert.AreEqual(_repo.Exists(transaction_2), false);
        }

        [TestMethod]
        public void TestGetFirstIncomeBeforeDate()
        {
            this.RemoveAll();
            var count = _repo.FindAll();
            DateTime date = DateTime.Now;
            Assert.AreEqual(0, count.Count(), "DB not empty");

            Transaction transaction_1 = new Transaction();
            transaction_1.CreditorName = "Henk";
            transaction_1.CreditorNumber = "555";
            transaction_1.Date = date;
            _repo.Add(transaction_1);

            var result = _repo.GetFirstIncomeBeforeDate(date.AddYears(1), transaction_1.CreditorNumber);
            Assert.AreEqual(transaction_1.CreditorName, result.CreditorName);
        }

        [TestMethod]
        public void TestGetFirstIncomeAfterDate()
        {
            this.RemoveAll();
            var count = _repo.FindAll();
            DateTime date = DateTime.Now.AddYears(1);
            Assert.AreEqual(0, count.Count(), "DB not empty");

            Transaction transaction_1 = new Transaction();
            transaction_1.CreditorName = "Henk";
            transaction_1.CreditorNumber = "555";
            transaction_1.Date = date;
            _repo.Add(transaction_1);

            var result = _repo.GetFirstIncomeBeforeDate(date, transaction_1.CreditorNumber);
            Assert.AreEqual(transaction_1.CreditorName, result.CreditorName);
        }
    /// <summary>
    /// Removes all data in DB.
    /// </summary>
        public void RemoveAll()
        {
            var all = _repo.FindAll();

            foreach (var item in all)
            {
                _repo.Delete(item);
            }

        }
    }
}
