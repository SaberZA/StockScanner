using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NaaStockTrader.Core.Interfaces;

namespace NaaStockTrader.Core.Services.Sql
{
    public class StockRepository : SQLiteRepository, IStockRepository
    {
        public StockRepository(ISQLiteConnection sqLiteConnection) : base(sqLiteConnection)
        {
            CreateTable();
        }

        public int CreateTable()
        {
            return CreateTable<StockItem>();
        }

        public int Delete(StockItem objectToDelete)
        {
            return Delete(objectToDelete);
        }

        public int DropTable()
        {
            return DropTable<StockItem>();
        }

        public int Insert(StockItem obj)
        {
            return Insert(obj);
        }

        public List<StockItem> Query(string query, params object[] args)
        {
            return Query(query, args);
        }
    }
}
