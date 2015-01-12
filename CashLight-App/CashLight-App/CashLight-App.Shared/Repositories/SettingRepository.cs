using AutoMapper;
using CashLight_App.Models;
using CashLight_App.Repositories.Interfaces;
using CashLight_App.Services.SQLite;
using CashLight_App.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CashLight_App.Repositories
{
    class SettingRepository : ISettingRepository
    {
        private ISQLiteService _db;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="SQLiteService"></param>
        public SettingRepository(ISQLiteService SQLiteService)
        {
            this._db = SQLiteService;

            Mapper.CreateMap<SettingTable, Setting>();
        }

        /// <summary>
        /// Get a Setting by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Setting FindByKey(string key)
        {
            TableQuery<SettingTable> settings = _db.Context.Table<SettingTable>();

            SettingTable setting = settings
                .Where(x => x.Key == key)
                .OrderByDescending(q => q.Date)
                .FirstOrDefault();

            return Mapper.Map<SettingTable, Setting>(setting);
        }

        /// <summary>
        /// Adds a new setting
        /// </summary>
        /// <param name="setting"></param>
        public void Add(Setting setting)
        {
            Mapper.CreateMap<Setting, SettingTable>();
            SettingTable settingTable = Mapper.Map<Setting, SettingTable>(setting);

            var existingRow = _db.Context.Table<SettingTable>()
                .Where(x => x.Key == setting.Key)
                .FirstOrDefault();


            if (existingRow == default(SettingTable))
            {
                _db.Context.Table<SettingTable>().Connection.Insert(settingTable);
            }
            else
            {
                settingTable.SettingID = existingRow.SettingID;
                _db.Context.Table<SettingTable>().Connection.Update(settingTable);
            }
        }

        /// <summary>
        /// Commit
        /// </summary>
        public void Commit()
        {
            _db.Context.Commit();
        }
    }
}
