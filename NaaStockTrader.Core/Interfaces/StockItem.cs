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
        private string _dateUpdated;      
        public string DateUpdated
        {
            get
            {
                return _dateUpdated;
            }
            set
            {
                _dateUpdated = value ?? DateTimeSQLite(DateTime.Now);
                int year = Int32.Parse(_dateUpdated.Substring(0, 4));
                int month = Int32.Parse(_dateUpdated.Substring(5, 2));
                int day = Int32.Parse(_dateUpdated.Substring(8, 2));
                int hour = Int32.Parse(_dateUpdated.Substring(11, 2));
                int minute = Int32.Parse(_dateUpdated.Substring(14, 2));
                int second = Int32.Parse(_dateUpdated.Substring(17, 2));
                DateUpdatedActual = new DateTime(year, month, day, hour, minute, second);
            }

        }

        [SQLite.Net.Attributes.Ignore]
        public DateTime DateUpdatedActual { get; set; }

        public string DateTimeSQLite(DateTime datetime)
        {
            string dateTimeFormat = "{0}-{1}-{2} {3}:{4}:{5}.{6}";
            return string.Format(dateTimeFormat, datetime.Year, 
                PadLeftString(datetime.Month.ToString()), 
                PadLeftString(datetime.Day.ToString()), 
                PadLeftString(datetime.Hour.ToString()),
                PadLeftString(datetime.Minute.ToString()),
                PadLeftString(datetime.Second.ToString()),
                PadLeftString(datetime.Millisecond.ToString(), 3));
        }

        private string PadLeftString(string number, int length = 2)
        {
            if (number.Length >= length)
            {
                return number;
            }
            else
            {
                number = "0" + number;
                return PadLeftString(number);
            }
        }
    }
    
}
