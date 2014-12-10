using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.DataModels
{
    [Table("Setting")]
    public class Setting
    {
        public Setting()
        {

        }
        public Setting(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
            this.Date = DateTime.Now;
        }
        public Setting(string Key, double Value)
        {
            this.Key = Key;
            this.Value = Value.ToString();
            this.Date = DateTime.Now;
        }

        [PrimaryKey, AutoIncrement]
        public int SettingID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }
    }
}
