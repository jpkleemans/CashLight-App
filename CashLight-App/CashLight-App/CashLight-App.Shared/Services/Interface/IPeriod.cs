using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Services.Interface
{
    interface IPeriod
    {

        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }

        void Next ();

        void Previous ();
    }
}
