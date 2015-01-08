using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using CashLight_App.Enums;
using CashLight_App.Config;
using CashLight_App.Services.SQLite;
using Autofac;
using Microsoft.Practices.ServiceLocation;
using CashLight_App.Tables;
using CashLight_App.Repositories.Interfaces;
using Windows.Storage;
using System.Threading.Tasks;
using System.Reflection;

namespace CashLight_Test.Windows
{
    [TestClass]
    public class UnitTest1
    {
        private StorageFile _csv;

        [TestInitialize]
        public void Initialize()
        {
            Bootstrapper.Initialize(Mode.Testing);
        }

        [TestMethod]
        public void TestMethod1()
        {
            //ISQLiteService db = ServiceLocator.Current.GetInstance<ISQLiteService>();

            //var jo1 = db.Context.Table<CategoryTable>();
            //Assert.AreEqual(0, jo1.Count());

            //var categoryTable = new CategoryTable("henk", CategoryType.Variable, 100);

            //db.Context.Table<CategoryTable>().Connection.Insert(categoryTable);

            //db.Context.Commit();

            //var jo2 = db.Context.Table<CategoryTable>();
            //Assert.AreEqual(1, jo2.Count());
            //Assert.AreEqual("henk", jo2.First().Name);
            //Assert.AreEqual(100, jo2.First().Budget);
            //Assert.AreEqual((int)CategoryType.Variable, jo2.First().Type);
            Assert.IsFalse(false);
        }

        [TestMethod]
        public void TestMethod2()
        {
            IUploadRepository uploadRepo = ServiceLocator.Current.GetInstance<IUploadRepository>();

            ITransactionRepository transactionRepo = ServiceLocator.Current.GetInstance<ITransactionRepository>();

            Assert.AreEqual(0, transactionRepo.FindAll().Count());

            Task<StorageFile> csv = StorageFile.GetFileFromPathAsync("/Assets/Backgrounds/Purple.jpg").AsTask();

            uploadRepo.ToDatabase(csv.Result);

            Assert.AreEqual(9, transactionRepo.FindAll().Count());
        }
    }
}
