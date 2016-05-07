using NaaStockTrader.Core.Interfaces;
using System.Collections.Generic;

namespace NaaStockTrader.Core.Services.Sql
{
    public interface IStockRepository : IRepository<StockItem>
    {
        void SeedStockItems(List<StockItem> stockItems);
    }
}