using NaaStockScanner.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockScanner.Core.Services.Csv
{
    public interface ICsvService
    {
        List<StockItem> GetRecords();
    }
}
