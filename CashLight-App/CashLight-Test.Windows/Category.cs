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
    public class Category
    {
        private StorageFile _csv;

        [TestInitialize]
        public void Initialize()
        {
            Bootstrapper.Initialize(Mode.Testing);
        }

        [TestMethod]
        public void TestAddCategory()
        {
            ISQLiteService db = ServiceLocator.Current.GetInstance<ISQLiteService>();

            var total = db.Context.Table<CategoryTable>();
            Assert.AreEqual(0, total.Count());

            var categoryTable = new CategoryTable("Category", CategoryType.Variable, 200.0);

            db.Context.Table<CategoryTable>().Connection.Insert(categoryTable);

            db.Context.Commit();

            var count = db.Context.Table<CategoryTable>();
            Assert.AreEqual(1, count.Count());
            Assert.AreEqual("Category", count.First().Name);
            //Assert.AreEqual(200.0, count.First().Budget);
            Assert.AreEqual((int)CategoryType.Variable, count.First().Type);
        }

        [TestMethod]
        public void TestEditCategory()
        {

            ISQLiteService db = ServiceLocator.Current.GetInstance<ISQLiteService>();

            var before = db.Context.Table<CategoryTable>();
            Assert.AreEqual(1, before.Count());
            Assert.AreEqual("Category", before.First().Name);

            var henkie = before.First();

            henkie.Name = "Changed Category";

            db.Context.Table<CategoryTable>().Connection.Update(henkie);

            var after = db.Context.Table<CategoryTable>();

            Assert.AreEqual(1, after.Count());
            Assert.AreEqual("Changed Category", before.First().Name);

        }

        [TestMethod]
        public void TestDeleteCategory()
        {

            ISQLiteService db = ServiceLocator.Current.GetInstance<ISQLiteService>();

            var before = db.Context.Table<CategoryTable>();
            Assert.AreEqual(1, before.Count());

            db.Context.Table<CategoryTable>().Connection.Delete(before.First());

            var after = db.Context.Table<CategoryTable>();
            Assert.AreEqual(0, after.Count());

        }

        //[TestMethod]
        //public void TestMethod2()
        //{
        //    IUploadRepository uploadRepo = ServiceLocator.Current.GetInstance<IUploadRepository>();

        //    ITransactionRepository transactionRepo = ServiceLocator.Current.GetInstance<ITransactionRepository>();

        //    Assert.AreEqual(0, transactionRepo.FindAll().Count());

        //    Task<StorageFile> csv = StorageFile.GetFileFromPathAsync("/Assets/Backgrounds/Purple.jpg").AsTask();

        //    uploadRepo.ToDatabase(csv.Result);

        //    Assert.AreEqual(9, transactionRepo.FindAll().Count());
        //}
    }
}
