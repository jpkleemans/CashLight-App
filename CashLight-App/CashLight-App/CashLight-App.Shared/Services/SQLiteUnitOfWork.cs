using CashLight_App.Tables;
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
        SQLiteRepository<TransactionTable> _transaction;
        SQLiteRepository<CategoryTable> _category;
        SQLiteRepository<SettingTable> _setting;

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

        public IRepository<TransactionTable> Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = new SQLiteRepository<TransactionTable>(_context);
                }
                return _transaction;
            }
        }

        public IRepository<CategoryTable> Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new SQLiteRepository<CategoryTable>(_context);
                }
                return _category;
            }
        }

        public IRepository<SettingTable> Setting
        {
            get
            {
                if (_setting == null)
                {
                    _setting = new SQLiteRepository<SettingTable>(_context);
                }
                return _setting;
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
            connection.CreateTable<CategoryTable>();
            connection.CreateTable<TransactionTable>();
            connection.CreateTable<SettingTable>();

            CashLight_App.Tables.CategoryTable c = new CashLight_App.Tables.CategoryTable("Vast");
            CashLight_App.Tables.CategoryTable c1 = new CashLight_App.Tables.CategoryTable("Variabel");
            CashLight_App.Tables.CategoryTable c2 = new CashLight_App.Tables.CategoryTable("Overig");

            connection.Insert(c);
            connection.Insert(c1);
            connection.Insert(c2);

            connection.Commit();
            return connection;
        }

        public void Dispose()
        {
            _context.Close();
        }
    }
}
