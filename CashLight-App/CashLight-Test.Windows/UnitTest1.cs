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

namespace CashLight_Test.Windows
{
    [TestClass]
    public class UnitTest1
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IPeriodModel> _periodModel;
        private DashboardViewModel _dashboardViewModel;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _periodModel = new Mock<IPeriodModel>();

            _dashboardViewModel = new DashboardViewModel(_unitOfWorkMock.Object, _periodModel.Object);

        }

        [TestMethod]
        public void TestMethod1()
        {
            Debug.WriteLine(_dashboardViewModel.ImportantIncomes);
        }
    }
}
