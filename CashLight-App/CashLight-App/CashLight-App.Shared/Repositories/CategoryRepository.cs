using AutoMapper;
using CashLight_App.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CashLight_App.Services.SQLite;
using CashLight_App.Tables;
using CashLight_App.Models;

namespace CashLight_App.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private ISQLiteService _db;

        public CategoryRepository(ISQLiteService SQLiteService)
        {
            this._db = SQLiteService;

            Mapper.CreateMap<CategoryTable, Category>();
        }

        public IEnumerable<Category> FindAll()
        {
            TableQuery<CategoryTable> table = _db.Context.Table<CategoryTable>();

            return Mapper.Map<IEnumerable<CategoryTable>, IEnumerable<Category>>(table);
        }
    }
}
