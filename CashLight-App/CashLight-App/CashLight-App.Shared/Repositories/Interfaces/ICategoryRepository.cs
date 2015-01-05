using CashLight_App.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashLight_App.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> FindAll();
        void Add(Category category);
    }
}
