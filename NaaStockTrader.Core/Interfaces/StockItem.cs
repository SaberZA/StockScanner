using NaaStockTrader.Core.Services.Sql;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Core.Interfaces
{
    public class StockItem : ISqlTable
    {
        [PrimaryKey, MaxLength(200)]
        public string StockCode { get; set; }

        [MaxLength(200)]
        public string BarCode { get; set; }

        [MaxLength(200)]
        public string StockDescription { get; set; }

        public int StockQuantity { get; set; }

        //("YYYY-MM-DD HH:MM:SS.SSS")        
        public DateTime DateUpdated { get; set; }
    }
    
}
