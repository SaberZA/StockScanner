using NaaStockScanner.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockScanner.Core.Services.Csv
{
    public class CoreCsvService : ICsvService
    {
        private List<StockItem> _records;
        private string _stockContent;

        public CoreCsvService(string stockContent)
        {
            _stockContent = stockContent;
            _records = GetRecords();
        }

        public List<StockItem> GetRecords()
        {
            var lines = _stockContent.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int dataItemCount = lines.Count() - 1;
            var headerLine = lines.First().ToLower();
            var dataLines = lines.Skip(1).ToList();

            var stockItems = new List<StockItem>();

            var headerProperties = headerLine.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //var stockCodeIndex = headerProperties.First(p => p == "stock code");
            //var barCodeIndex = headerProperties.First(p => p == "bar code");
            //var stockDescriptionIndex = headerProperties.First(p => p == "stock description");
            //var quantityIndex = headerProperties.First(p => p == "count");

            foreach (var stockLine in dataLines)
            {
                var stockProperties = stockLine.Split(new[] { ";" },StringSplitOptions.None);

                var stockQuantityIndex = 4;
                var stockPrice = string.IsNullOrEmpty(stockProperties[3]) ? "0" : stockProperties[3].Trim();
                // There is a formatted currency
                if (stockProperties.Length > 5)
                {
                    stockPrice = (stockProperties[3] + stockProperties[4]).Trim();
                    stockPrice = stockPrice.Substring(1, stockPrice.Length - 2);

                    stockQuantityIndex = 5;
                }

                
                if (stockPrice.StartsWith("R"))
                {
                    stockPrice = stockPrice.Substring(1).Trim();
                }

                var stockItem = new StockItem()
                {
                    StockCode = stockProperties[0],
                    BarCode = stockProperties[1],
                    StockDescription = stockProperties[2],
                    StockPrice = stockPrice,
                    StockQuantity = Int32.Parse(string.IsNullOrEmpty(stockProperties[stockQuantityIndex]) ? "0" : stockProperties[stockQuantityIndex])
                };

                stockItems.Add(stockItem);
            }
            return stockItems;
        }
    }
}
