using AutoMapper;
using CashLight_App.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SQLite;
using CashLight_App.Models.Interface;

namespace CashLight_App.Business
{
    public class CategoryRepository : RepositoryBase
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

    }
}
