﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Enums
{
    public class PeriodDTO
    {
        public PeriodDTO(string name, string account, double averagedeviation, double averageperiod)
        {
            this.Name = name;
            this.Account = account;
            this.AverageDeviation = averagedeviation;
            this.AveragePeriod = averageperiod;
        }

        public PeriodDTO()
        {

        }
        public string Name { get; set; }
        public string Account { get; set; }
        public double AverageDeviation { get; set; }
        public double AveragePeriod { get; set; }
    }
}