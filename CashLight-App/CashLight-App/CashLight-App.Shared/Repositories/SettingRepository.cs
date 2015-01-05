using AutoMapper;
using CashLight_App.Models;
using CashLight_App.Repositories.Interfaces;
using CashLight_App.Services.SQLite;
using CashLight_App.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Repositories
{
    class SettingRepository : ISettingRepository
    {
        private ISQLiteService _db;

        public SettingRepository(ISQLiteService SQLiteService)
        {
            this._db = SQLiteService;

            Mapper.CreateMap<SettingTable, Setting>();
        }

        public Setting FindByKey(string key)
        {
            TableQuery<SettingTable> settings = _db.Context.Table<SettingTable>();

            SettingTable setting = settings
                .Where(x => x.Key == key)
                .OrderByDescending(q => q.Date)
                .FirstOrDefault();

            return Mapper.Map<SettingTable, Setting>(setting);
        }

        public void Add(Setting setting)
        {
            Mapper.CreateMap<Setting, SettingTable>();
            SettingTable settingTable = Mapper.Map<Setting, SettingTable>(setting);

            _db.Context.Table<SettingTable>().Connection.Insert(settingTable);
        }

        public void Commit()
        {
            _db.Context.Commit();
        }
    }
}
