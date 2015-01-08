using GalaSoft.MvvmLight;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models
{
    public class Category : ObservableObject
    {
        public int CategoryID { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public double Budget { get; set; }

        public double Percentage { get; set; }
    }
}
