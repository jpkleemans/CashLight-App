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
    public class AccountRepository
    {

        public IAccountRepository _repo { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Bootstrapper.Initialize(Mode.Testing);
            _repo = ServiceLocator.Current.GetInstance<IAccountRepository>();

        }

        [TestMethod]
        public void TestAddAccount()
        {

        }

        [TestMethod]
        public void TestFindAllAccount()
        {

        }


        [TestMethod]
        public void TestFindAllCategorizedAccount()
        {

        }

        [TestMethod]
        public void TestFindAllCategorizedAccount()
        {

        }


        [TestMethod]
        public void TestDeleteAccount()
        {

        }



    }
}
