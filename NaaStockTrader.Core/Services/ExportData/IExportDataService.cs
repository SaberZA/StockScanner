using NaaStockScanner.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Core.Services.ExportData
{
    public interface IExportDataService
    {
        void SaveCsvToDevice(List<StockItem> stockItems);
    }
}
