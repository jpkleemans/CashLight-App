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
using CashLight_App.Models;
using System.Diagnostics;

namespace CashLight_Test.Windows
{

    [TestClass]
    public class CategoryRepository
    {

        public ICategoryRepository _repo { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Bootstrapper.Initialize(Mode.Testing);
            _repo = ServiceLocator.Current.GetInstance<ICategoryRepository>();

        }

        [TestMethod]
        public void TestAddCategory()
        {
            this.RemoveAll();
            var count = _repo.FindAll();
            Assert.AreEqual(0, count.Count());

            Category category = new Category();
            category.Name = "Test Category 1";
            category.Type = (int)CategoryType.Fixed;

            _repo.Add(category);

            count = _repo.FindAll();
            Assert.AreEqual(1, count.Count());
            
        }

        [TestMethod]
        public void TestEditCategory()
        {
            this.RemoveAll();
            var count = _repo.FindAll();
            Assert.AreEqual(0, count.Count());

            Category category = new Category();
            category.Name = "Test Category 1";
            category.Type = (int)CategoryType.Fixed;

            _repo.Add(category);

            var result = _repo.FindAll().First();
            Debug.WriteLine(result.Name);

            //Assert.AreEqual("Test Category 1", result.Name);
            //Assert.AreEqual((int)CategoryType.Fixed, result.Type);

            result.Name = "Test Category Modified";

            _repo.Edit(result);

            var modified = _repo.FindByID(1);
            Debug.WriteLine(modified.Name);
            Assert.AreEqual("Test Category Modified", modified.Name);

        }

        [TestMethod]
        public void TestFindAllCategory()
        {
            this.RemoveAll();

            var count = _repo.FindAll();
            Assert.AreEqual(0, count.Count());

            Category category_1 = new Category();
            category_1.Name = "Test Category 1";
            category_1.Type = (int)CategoryType.Fixed;
            _repo.Add(category_1);

            Category category_2 = new Category();
            category_2.Name = "Test Category 2";
            category_2.Type = (int)CategoryType.Variable;
            category_2.Budget = 200.00;
            _repo.Add(category_2);

            var all = _repo.FindAll().ToList();

            Assert.AreEqual(2, all.Count());
            Assert.AreEqual("Test Category 1", all[0].Name);
            Assert.AreEqual("Test Category 2", all[1].Name);

            Assert.AreEqual((int)CategoryType.Fixed, all[0].Type);
            Assert.AreEqual((int)CategoryType.Variable, all[1].Type);
            Assert.AreEqual(200.00, all[1].Budget);

        }

        [TestMethod]
        public void TestDeleteCategory()
        {
            this.RemoveAll();

            var count = _repo.FindAll();
            Assert.AreEqual(0, count.Count());

            Category category_1 = new Category();
            category_1.Name = "Test Category 1";
            category_1.Type = (int)CategoryType.Fixed;
            _repo.Add(category_1);

            count = _repo.FindAll();
            Assert.AreEqual(1, count.Count());

            _repo.Delete(count.First());

            count = _repo.FindAll();
            Assert.AreEqual(0, count.Count());

        }

        [TestMethod]
        public void TestFindByIDCategory()
        {
            this.RemoveAll();

            var count = _repo.FindAll();
            Assert.AreEqual(0, count.Count());

            Category category_1 = new Category();
            category_1.Name = "Test Category 1";
            category_1.Type = (int)CategoryType.Fixed;
            _repo.Add(category_1);

            Category category_2 = new Category();
            category_2.Name = "Test Category 2";
            category_2.Type = (int)CategoryType.Variable;
            category_2.Budget = 200.00;
            _repo.Add(category_2);

            var all = _repo.FindAll().ToList();

            Assert.AreEqual(2, all.Count());
            Assert.AreEqual("Test Category 1", all[0].Name);
            Assert.AreEqual("Test Category 2", all[1].Name);

            Assert.AreEqual((int)CategoryType.Fixed, all[0].Type);
            Assert.AreEqual((int)CategoryType.Variable, all[1].Type);
            Assert.AreEqual(200.00, all[1].Budget);
        }

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
