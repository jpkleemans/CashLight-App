using CashLight_App.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Config
{
    class Bootstrapper
    {
        public Bootstrapper()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<DashboardViewModel>();
        }
    }
}
