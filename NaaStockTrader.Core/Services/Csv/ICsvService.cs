using NaaStockTrader.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Core.Services.Csv
{
    public interface ICsvService
    {
        List<StockItem> GetRecords();
    }
}
