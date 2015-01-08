using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        void Edit(Category category);
        Category FindByID(int id);
        Category FindByName(string name);
        IEnumerable<Category> FindAll();
        void Add(Category category);
        void Delete(Category category);
        void Commit();
    }
}
