using AutoMapper;
using CashLight_App.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CashLight_App.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        //public CategoryRepository()
        //{
        //    Mapper.CreateMap<ICategory, ICategory>();
        //}

        //public IEnumerable<ICategory> FindAll()
        //{
        //    TableQuery<Category> table = this._context.Table<Category>();
        //    return Mapper.Map<IEnumerable<Category>, IEnumerable<Category>>(table);
        //}

        public IEnumerable<Models.Category> FindAll()
        {
            throw new NotImplementedException();
        }
    }
}
