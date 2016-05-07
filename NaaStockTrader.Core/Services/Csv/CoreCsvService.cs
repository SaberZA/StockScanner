﻿using NaaStockTrader.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Core.Services.Csv
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
            var dataLines = lines.Skip(1).ToList();

            var stockItems = new List<StockItem>();

            foreach (var stockLine in dataLines)
            {
                var stockProperties = stockLine.Split(new[] { ";" },StringSplitOptions.None);
                var stockItem = new StockItem()
                {
                    StockCode = stockProperties[0],
                    BarCode = stockProperties[1],
                    StockDescription = stockProperties[2],
                    StockQuantity = Int32.Parse(string.IsNullOrEmpty(stockProperties[3]) ? "0" : stockProperties[3])
                };

                stockItems.Add(stockItem);
            }
            return stockItems;
        }
    }
}
