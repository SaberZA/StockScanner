using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaaStockScanner.Core.Interfaces;

namespace NaaStockScanner.Core.Services.Sql
{
    public class StockRepository : SQLiteRepository, IStockRepository
    {
        public StockRepository(ISQLiteConnection sqLiteConnection) : base(sqLiteConnection)
        {
            CreateTable();
            CreateStockIndexes(sqLiteConnection);
        }

        private void CreateStockIndexes(ISQLiteConnection sqLiteConnection)
        {
            var result = sqLiteConnection.GetConnection().Execute("CREATE INDEX IF NOT EXISTS IdxStockCodeBarCode ON StockItem(StockCode, BarCode);");
        }

        public int CreateTable()
        {

            return base.CreateTable<StockItem>();
        }

        public int Delete(StockItem objectToDelete)
        {
            return base.Delete(objectToDelete);
        }

        public int DropTable()
        {
            return base.DropTable<StockItem>();
        }

        public int Insert(StockItem obj)
        {
            return base.Insert(obj);
        }

        public List<StockItem> Query(string query, params object[] args)
        {
            return base.Query<StockItem>(query, args);
        }

        public void SeedStockItems(List<StockItem> stockItems)
        {
            //TODO: Should actually get the diff and add new items to db
            var stockItemsInDb = Query("select StockCode,BarCode,StockDescription,StockQuantity,DateUpdated from StockItem");

            var stockItemsToAdd = new List<StockItem>();

            foreach (var stockItem in stockItems)
            {
                if (!stockItemsInDb.Any(p=>p.StockCode == stockItem.StockCode))
                {
                    stockItemsToAdd.Add(stockItem);
                    //var result = GetConnection().Insert(stockItem);
                }
            }

            //GetConnection().InsertAll(stockItemsToAdd);
            foreach (var stockItem in stockItemsToAdd)
            {
                var insertedRows = GetConnection().InsertOrReplace(stockItem);

            }

            //if (!stockItemsInDb.Any())
            //{
            //    StringBuilder sb = new StringBuilder();
            //    var result = GetConnection().InsertAll(stockItems);
            //    //foreach (var item in stockItems)
            //    //{
            //    //    var rowsInserted = Insert(item);

            //        //sb.AppendLine(string.Format("INSERT INTO StockItem (StockCode,BarCode,StockDescription,StockQuantity,DateUpdated) VALUES ('{0}','{1}','{2}','{3}','{4}');",
            //        //    item.StockCode,
            //        //    item.BarCode,
            //        //    item.StockDescription,
            //        //    item.StockQuantity,
            //        //    item.DateTimeSQLite(DateTime.Now)));
            //    //}
            //    //base.Execute(sb.ToString());

            //}
        }

        
    }
}
