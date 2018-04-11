using ClassLibrary1.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace ClassLibrary3.Database
{
    public class SQLiteOpenHelper
    {
        private SQLiteConnection sqlite;

        public SQLiteConnection Sqlite
        {
            get { return sqlite; }
        }

        public SQLiteOpenHelper()
        {
        }

        private void CreateTable()
        {
            try
            {
                this.sqlite.CreateTable<User>();
                this.sqlite.CreateTable<Product>();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void Insert(Object o)
        {
            this.sqlite.InsertOrReplace(o);
        }



        private bool isInitializing = false;
        public SQLiteConnection GetWritableDatabase()
        {
            if (!this.isInitializing && this.sqlite == null)
            {
                this.isInitializing = true;
                ////TODO get from config file.
                String databasePath = ApplicationData.Current.LocalFolder.Path
                    + "\\app3db.sqlite";

                // Check before create connection (create connection create file !!)
                bool isExist = File.Exists(databasePath);
                SQLiteConnection db = new SQLiteConnection(databasePath);

                // Check if database exit
                if (!isExist)
                {
                    this.sqlite = db;
                    this.CreateTable();
                }
                this.isInitializing = false;
                return this.sqlite;
            }
            return null;
        }
    }
}
