using CashLight_App.Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Business.Interfaces
{
    public interface IPeriodRepository
    {
        IPeriod GetByDate(DateTime date);
    }
}
