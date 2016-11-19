using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NaaStockScanner.Core.Interfaces;
using SQLite;

namespace NaaStockScanner.DbConvert
{
    class Program
    {
        //Build Configuration must be set to x86 or x64 for SQLite
        static void Main(string[] args)
        {
            var sourceDbName = "27Aug2016";
            var targetDbName = sourceDbName;

            IEnumerable<StockItem> results;
            using (var conn = new OleDbConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={sourceDbName}.mdb"))
            {
                conn.Open();
                results = conn.Query<StockItem>(
                    "select * from StockItem"
                    );
                conn.Close();
            }

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
