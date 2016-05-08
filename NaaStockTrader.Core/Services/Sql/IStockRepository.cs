using NaaStockScanner.Core.Interfaces;
using System.Collections.Generic;

namespace NaaStockScanner.Core.Services.Sql
{
    public interface IStockRepository : IRepository<StockItem>
    {
        void SeedStockItems(List<StockItem> stockItems);
    }
}