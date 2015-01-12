using CashLight_App.Services.SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Tables
{
    [Table("Setting")]
    public class SettingTable
    {
        [PrimaryKey, AutoIncrement]
        public int SettingID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }

        public SettingTable()
        {

        }

        public SettingTable(string Key, string Value)
        {
            this.Key = Key;
            this.Value = Value;
            this.Date = DateTime.Now;
        }

        public SettingTable(string Key, double Value)
            : this(Key, Value.ToString())
        {
        }
    }
}
