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

        }

        public void Edit(Category category)
        {
            Mapper.CreateMap<Category, CategoryTable>();
            CategoryTable categoryTable = Mapper.Map<Category, CategoryTable>(category);

            _db.Context.Table<CategoryTable>().Connection.Update(categoryTable);
        }

        public IEnumerable<Category> FindAll()
        {
            Mapper.CreateMap<CategoryTable, Category>();
            TableQuery<CategoryTable> table = _db.Context.Table<CategoryTable>();

            return Mapper.Map<IEnumerable<CategoryTable>, IEnumerable<Category>>(table);
        }

        public void Add(Category category)
        {
            Mapper.CreateMap<Category, CategoryTable>();
            CategoryTable categoryTable = Mapper.Map<Category, CategoryTable>(category);

            _db.Context.Table<CategoryTable>().Connection.Insert(categoryTable);
        }
        public void Delete(Category category)
        {
            CategoryTable CategoryTable = new CategoryTable();

            CategoryTable.CategoryID = category.CategoryID;

            _db.Context.Table<CategoryTable>().Connection.Delete(CategoryTable);
        }


        public void Commit()
        {
            _db.Context.Commit();
        }

        public Category FindByName(string name)
        {
            Mapper.CreateMap<CategoryTable, Category>();
            var c = _db.Context.Table<CategoryTable>().Where(q => q.Name == name).FirstOrDefault();

            return Mapper.Map<CategoryTable, Category>(c);
        }

        public Category FindByID(int id)
        {
            Mapper.CreateMap<CategoryTable, Category>();
            var c = _db.Context.Table<CategoryTable>().Where(q => q.CategoryID == id).FirstOrDefault();

            return Mapper.Map<CategoryTable, Category>(c);
        }

    }
}
