using System;
using System.Threading.Tasks;
using Windows.Storage;
using CashLight_App.Tables;
using CashLight_App.Enums;
using System.Collections.Generic;

namespace CashLight_App.Services.SQLite
{
    public class SQLiteService : ISQLiteService, IDisposable
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

        public SQLiteService(string name)
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

            List<CategoryTable> defaultCategories = new List<CategoryTable>()
            {
                new CategoryTable("Energie", CategoryType.Fixed, 60),
                new CategoryTable("Wonen", CategoryType.Fixed, 20),
                new CategoryTable("Boodschappen", CategoryType.Fixed, 10),
                new CategoryTable("Kleding", CategoryType.Variable, 5),
                new CategoryTable("Vrije tijd", CategoryType.Variable, 30),
                new CategoryTable("Overig", CategoryType.Other, 0)
            };

            foreach (CategoryTable categorie in defaultCategories)
            {
                connection.Insert(categorie);
            }

            connection.Commit();

            return connection;
        }

        public void Dispose()
        {
            _context.Close();
        }
    }
}
