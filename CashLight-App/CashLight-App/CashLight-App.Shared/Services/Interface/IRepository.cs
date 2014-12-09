using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CashLight_App.Services.Interface
{
    public interface IRepository<T>
    {
        //Alle rows uit de tabel ophalen
        IQueryable<T> FindAll();

        //LINQ methode Any
        bool Any(Expression<Func<T, bool>> predicate);

        //LINQ methode Find
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        //Toevoegen nieuwe record
        void Add(T newEntity);

        //Verwijderen record
        void Delete(T Entity);

        void DeleteAll();
    }
}
