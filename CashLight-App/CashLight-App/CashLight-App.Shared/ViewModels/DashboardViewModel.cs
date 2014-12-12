using CashLight_App.DataModels;
using CashLight_App.Models;
using CashLight_App.Models.Interfaces;
using CashLight_App.Services.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CashLight_App.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public ObservableCollection<IPeriodModel> Periods { get; set; }

        public DashboardViewModel()
        {
            Periods = new ObservableCollection<IPeriodModel>();

            InitPeriods();
            }

        private void InitPeriods()
        {
            IPeriodModel now = new PeriodModel(new DateTime(2014, 10, 01));
            IPeriodModel prev = now.Previous();
            IPeriodModel next = now.Next();

            Periods.Add(prev);
            Periods.Add(now);
            Periods.Add(next);
        }

        public void GoToPreviousPeriod()
        {
            //
        }

        public void GoToNextPeriod()
        {
            //
        }
    }
}
