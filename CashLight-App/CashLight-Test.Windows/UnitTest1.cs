using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using NUnit.Framework;
using Moq;
using CashLight_App.Services.Interface;
using CashLight_App.ViewModels;
using CashLight_App.Models.Interfaces;
using System.Diagnostics;
using CashLight_App.Models;

namespace CashLight_Test.Windows
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IPeriodModel> _periodModel;

        [SetUp]
        public void SetUp()
        {
        }

        [TestMethod]
        public void TestMethod1()
        {
            _periodModel = new Mock<IPeriodModel>();
            //_periodModel.Setup(x => x.Next()).Returns(new PeriodModel());

            IPeriodModel model = _periodModel.Object;

            var newPeriod = model.Next();

            Debug.WriteLine(newPeriod);

            Microsoft.VisualStudio.TestPlatform.UnitTestFramework.Assert.IsInstanceOfType(_periodModel.Object.Next(), typeof(IPeriodModel));

        }
    }
}
