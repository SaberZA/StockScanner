using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NaaStockScanner.Core.Services.Sql;
using SQLite.Net;
using PCLStorage;
using System.IO;
using SQLite.Net.Platform.XamarinAndroid;

namespace NaaStockScanner.Droid.Services.Sqlite
{
    public class SQLiteConnectionAndroid : ISQLiteConnection
    {
        private readonly string _dbPath;

        public SQLiteConnectionAndroid(string dbPath)
        {
            _dbPath = dbPath;
        }

        public SQLiteConnection GetConnection()
        {
            var localStorage = FileSystem.Current.LocalStorage;
            var documentsPath = localStorage.Path;
            var path = Path.Combine(documentsPath, _dbPath);

            var platform = new SQLitePlatformAndroid();
            var connection = new SQLiteConnection(platform, path);

            return connection;
        }        
    }
}