using System;
using System.Collections.Generic;
using System.Text;
using CashLight_App.Models.Interface;

namespace CashLight_App.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<ICategory> FindAll();
    }
}
