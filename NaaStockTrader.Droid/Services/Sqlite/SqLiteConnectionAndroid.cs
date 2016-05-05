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
using NaaStockTrader.Core.Services.Sql;
using SQLite.Net;
using PCLStorage;
using System.IO;
using SQLite.Net.Platform.XamarinAndroid;

namespace NaaStockTrader.Droid.Services.Sqlite
{
    public class SQLiteConnectionAndroid : ISQLiteConnection
    {
        public SQLiteConnectionAndroid()
        {
        }

        public SQLiteConnection GetConnection()
        {
            var fileName = "naastock.sqlite";
            var localStorage = FileSystem.Current.LocalStorage;
            var documentsPath = localStorage.Path;
            var path = Path.Combine(documentsPath, fileName);

            var platform = new SQLitePlatformAndroid();
            var connection = new SQLiteConnection(platform, path);

            return connection;
        }        
    }
}