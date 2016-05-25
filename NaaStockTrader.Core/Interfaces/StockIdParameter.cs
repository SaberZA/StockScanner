namespace NaaStockScanner.Core.Interfaces
{
    public class StockIdParameter
    {        
        public string StockId { get; set; }
        public string StockDescription { get; set; }
        public int StockQuantity { get; set; }
        public string StockPrice { get; set; }
        //public StockItem StockItem { get; internal set; }
    }
}