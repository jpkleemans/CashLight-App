using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.ViewModel
{
    public class DashboardViewModel : ViewModelBase
    {
        private string _helloWorld;
        public string HelloWorld
        {
            get
            {
                return _helloWorld;
            }
            set
            {
                _helloWorld = value;
                RaisePropertyChanged(() => HelloWorld);
            }
        }

        public DashboardViewModel()
        {
            HelloWorld = "Hello World!";
        }
    }
}
