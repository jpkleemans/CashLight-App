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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="SQLiteService"></param>
        public CategoryRepository(ISQLiteService SQLiteService)
        {
            this._db = SQLiteService;

        }

        /// <summary>
        /// Edits (Saves) a specific category
        /// </summary>
        /// <param name="category"></param>
        public void Edit(Category category)
        {
            Mapper.CreateMap<Category, CategoryTable>();
            CategoryTable categoryTable = Mapper.Map<Category, CategoryTable>(category);

            _db.Context.Table<CategoryTable>().Connection.Update(categoryTable);
        }

        /// <summary>
        /// This method will do a FindAll-query on the database
        /// </summary>
        /// <returns>IEnumerable<Category></returns>
        public IEnumerable<Category> FindAll()
        {
            Mapper.CreateMap<CategoryTable, Category>();
            TableQuery<CategoryTable> table = _db.Context.Table<CategoryTable>();

            return Mapper.Map<IEnumerable<CategoryTable>, IEnumerable<Category>>(table);
        }

        /// <summary>
        /// Adds a new category to the database
        /// </summary>
        /// <param name="category"></param>
        public void Add(Category category)
        {
            Mapper.CreateMap<Category, CategoryTable>();
            CategoryTable categoryTable = Mapper.Map<Category, CategoryTable>(category);

            _db.Context.Table<CategoryTable>().Connection.Insert(categoryTable);
        }

        /// <summary>
        /// Removes a specific category from the database
        /// </summary>
        /// <param name="category"></param>
        public void Delete(Category category)
        {
            CategoryTable CategoryTable = new CategoryTable();
            CategoryTable.CategoryID = category.CategoryID;

            _db.Context.Table<CategoryTable>().Connection.Delete(CategoryTable);
        }

        /// <summary>
        /// Commit-method
        /// </summary>
        public void Commit()
        {
            _db.Context.Commit();
        }

        /// <summary>
        /// Query; Find by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Category</returns>
        public Category FindByName(string name)
        {
            Mapper.CreateMap<CategoryTable, Category>();
            var c = _db.Context.Table<CategoryTable>().Where(q => q.Name == name).FirstOrDefault();

            return Mapper.Map<CategoryTable, Category>(c);
        }

        /// <summary>
        /// Query; Find by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category</returns>
        public Category FindByID(int id)
        {
            Mapper.CreateMap<CategoryTable, Category>();
            var c = _db.Context.Table<CategoryTable>().Where(q => q.CategoryID == id).FirstOrDefault();

            return Mapper.Map<CategoryTable, Category>(c);
        }

    }
}
