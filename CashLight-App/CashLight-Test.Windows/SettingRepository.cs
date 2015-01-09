using CashLight_App.Config;
using CashLight_App.Enums;
using CashLight_App.Models;
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
    public class SettingRepository
    {

        public ISettingRepository _repo { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Bootstrapper.Initialize(Mode.Testing);
            _repo = ServiceLocator.Current.GetInstance<ISettingRepository>();

        }

        [TestMethod]
        public void TestAddSetting()
        {

            new Setting("Key01", "Value01");
            new Setting("Key02", "Value02");

            Assert.AreEqual("Value01", _repo.FindByKey("Key01"));
            Assert.AreEqual("Value02", _repo.FindByKey("Key02"));

        }

        [TestMethod]
        public void TestFindByKeySetting()
        {

        }


    }
}
