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

namespace CashLight_Test.Windows
{
    [TestClass]
    public class UploadRepository
    {
        private ITransactionRepository _transactions { get; set; }
           
        public IUploadRepository _repo { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Bootstrapper.Initialize(Mode.Testing);
            _repo = ServiceLocator.Current.GetInstance<IUploadRepository>();

            _transactions = ServiceLocator.Current.GetInstance<ITransactionRepository>();

        }

        [TestMethod]
        public void TestSaveTransaction()
        {

            var count = _transactions.FindAll();
            Assert.AreEqual(0, count.Count());

            var transaction = new Dictionary<string, string>();

            transaction["Af / Bij"] = "Af";
            transaction["Datum"] = "12-12-2015 00:00:00";
            transaction["Bedrag (EUR)"] = "200.00";
            transaction["Tegenrekening"] = "42345234234";
            transaction["Mededelingen"] = "Message";
            transaction["Naam / Omschrijving"] = "Description";
            transaction["Rekening"] = "23353453452";

            _repo.SaveTransaction(transaction);

            count = _transactions.FindAll();

            Assert.AreEqual(1, count.Count());


        }

    }
}
