using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Models
{
    public class Setting
    {
        public int SettingID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }
    }
}
