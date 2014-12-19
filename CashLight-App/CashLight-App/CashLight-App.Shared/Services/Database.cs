using CashLight_App.Services.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using CashLight_App.Tables;

namespace CashLight_App.Services
{
    public class Database : IDatabase, IDisposable
    {
        SQLiteConnection _context;
        public SQLiteConnection Context
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }

        public Database(string name)
        {
            string applicationFolderPath = Windows.Storage.ApplicationData.Current.LocalFolder.Path;

            // Create the folder path.
            string databaseFileName = System.IO.Path.Combine(applicationFolderPath, name);

            SQLite.SQLite3.Config(SQLite.SQLite3.ConfigOption.Serialized);

            bool exists = DoesDbExist(name).Result;
            if (exists)
            {
                _context = new SQLiteConnection(databaseFileName);
            }
            else
            {
                _context = this.CreateDatabase(databaseFileName);
            }
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

            CategoryTable c = new CategoryTable("Vast");
            CategoryTable c1 = new CategoryTable("Variabel");
            CategoryTable c2 = new CategoryTable("Overig");

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
