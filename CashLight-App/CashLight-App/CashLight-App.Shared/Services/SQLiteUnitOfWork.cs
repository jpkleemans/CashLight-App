using CashLight_App.DataModels;
using CashLight_App.Services.Interface;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CashLight_App.Services
{
    public class SQLiteUnitOfWork : IUnitOfWork, IDisposable
    {
        SQLiteRepository<Transaction> _transaction;
        SQLiteRepository<Category> _category;

        SQLiteConnection _context;

        public SQLiteUnitOfWork(string _dbname)
        {
            string applicationFolderPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;

            var dbname = _dbname;
            // Create the folder path.
            string databaseFileName = System.IO.Path.Combine(applicationFolderPath, dbname);

            SQLite.SQLite3.Config(SQLite.SQLite3.ConfigOption.Serialized);


            bool exists = DoesDbExist(dbname).Result;
            if (exists)
            {
                _context = new SQLiteConnection(databaseFileName);
            }
            else
            {
                _context = this.CreateDatabase(databaseFileName);
            }
        }

        public IRepository<Transaction> Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = new SQLiteRepository<Transaction>(_context);
                }
                return _transaction;
            }
        }

        public IRepository<Category> Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new SQLiteRepository<Category>(_context);
                }
                return _category;
            }
        }

        public void Commit()
        {
            _context.Commit();
        }

        public async Task<bool> DoesDbExist(string DatabaseName)
        {
            bool dbexist = true;
            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(DatabaseName);
            }
            catch
            {
                dbexist = false;
            }

            return dbexist;
        }

        public SQLiteConnection CreateDatabase(string database)
        {
            SQLiteConnection connection = new SQLiteConnection(database);
            connection.CreateTable<Category>();
            connection.CreateTable<Transaction>();
            return connection;
        }

        public void Dispose()
        {
            _context.Close();
        }
    }
}
