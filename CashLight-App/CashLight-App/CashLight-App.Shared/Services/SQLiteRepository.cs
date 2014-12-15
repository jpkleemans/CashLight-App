using CashLight_App.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CashLight_App.Services
{
    public class SQLiteRepository<T> : IRepository<T> where T : class, new()
    {
        private SQLite.SQLiteConnection _context;
        public SQLiteRepository(SQLite.SQLiteConnection context)
        {
            _context = context;
        }

        /// <summary>
        /// Haalt alle rijen van deze tabel op
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> FindAll()
        {
            return this._context.Table<T>().AsQueryable();
            //this._context.Table<T>().AsQueryable();
        }

        /// <summary>
        /// Voert de LINQ methode Find uit op de tabel
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns></returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            var context = _context.Table<T>().Where(predicate).AsQueryable();
            return context;
        }

        /// <summary>
        /// Voegt een nieuwe record toe aan de tabel
        /// </summary>
        /// <param name="newEntity">Nieuwe record</param>
        public void Add(T newEntity)
        {
            _context.Table<T>().Connection.Insert(newEntity);
        }

        /// <summary>
        /// Verwijdert een record uit de database
        /// </summary>
        /// <param name="Entity">Record</param>
        public void Delete(T Entity)
        {

            _context.Table<T>().Connection.Delete(Entity);
        }

        /// <summary>
        /// Voert de LINQ methode Any uit op de tabel
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> predicate)
        {
            var query = _context.Table<T>().AsQueryable();
            return query.Any(predicate);
        }

        public void Edit(T Entity)
        {
            _context.Table<T>().Connection.Update(Entity);
        }
        public void DeleteAll()
        {
            //ObjectContext objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)_context).ObjectContext;
            //Type entityType = typeof(T);

            //if (entityType.BaseType != null && entityType.Namespace == "System.Data.Entity.DynamicProxies")
            //    entityType = entityType.BaseType;

            //string entityTypeName = entityType.Name;

            //EntityContainer container =
            //    objCtx.MetadataWorkspace.GetEntityContainer(objCtx.DefaultContainerName, DataSpace.CSpace);
            //string entitySetName = (from meta in container.BaseEntitySets
            //                        where meta.ElementType.Name == entityTypeName
            //                        select meta.Name).First();
            //var tblnaam = entitySetName;

            ////var 
            ////var command = "DELETE FROM " + tblnaam + " DBCC CHECKIDENT ('" + tblnaam+ "',RESEED, 0)";
            //var command = "DELETE FROM " + tblnaam;
            //objCtx.ExecuteStoreCommand(command);
            ////command = "TRUNCATE TABLE " + tblnaam + "";
            ////objCtx.ExecuteStoreCommand(command);
        }


    }
}
