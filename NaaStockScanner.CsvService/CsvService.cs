using CsvHelper;
using NaaStockTrader.Core.Interfaces;
using NaaStockTrader.Core.Services.Csv;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockScanner.CsvService
{
    public class CsvService : ICsvService
    {
        private string _stockContent;

        public CsvService(string stockContent)
        {
            _stockContent = stockContent;
        }

        public List<StockItem> GetRecords()
        {
            var csv = new CsvReader(new StringReader(_stockContent));
            var records = csv.GetRecords<StockItem>();
            return records.ToList();
        }
    }
}
