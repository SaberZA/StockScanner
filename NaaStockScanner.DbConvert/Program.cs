using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NaaStockScanner.Core.Interfaces;
using SQLite;
using System.IO;
using Newtonsoft.Json;

namespace NaaStockScanner.DbConvert
{
    class Program
    {
        //Build Configuration must be set to x86 or x64 for SQLite
        static void Main(string[] args)
        {
            
            var sourceDbName = "07May2017";
            var dbExtension = "accdb";
            IEnumerable<StockItem> results;
            //using (var conn = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={sourceDbName}.{dbExtension}"))
            //{
            //    conn.Open();
            //    results = conn.Query<StockItem>(
            //        "select * from StockItem"
            //        );
            //    conn.Close();
            //}

            var jsonStringArray = File.ReadAllText("stock-07May2017.json");
            results = JsonConvert.DeserializeObject<IEnumerable<StockItem>>(jsonStringArray);
            
            var targetDbName = sourceDbName + "-test";

            using (var conn = new SQLiteConnection($"{targetDbName}.sqlite"))
            {
                conn.CreateTable<StockItem>();
                conn.DeleteAll<StockItem>();
                conn.InsertAll(results, typeof(StockItem));
            }

            using (var conn = new SQLiteConnection($"{targetDbName}.sqlite"))
            {
                var stockItems = conn.Table<StockItem>();
                Console.WriteLine("StockItems: {0}", stockItems.Count());
            }

            Console.Read();
        }
    }
}
