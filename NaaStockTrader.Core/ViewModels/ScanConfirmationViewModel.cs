using MvvmCross.Core.ViewModels;
using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Interfaces;
using NaaStockScanner.Core.Services.Csv;
using NaaStockScanner.Core.Services.Sql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockScanner.Core.ViewModels
{
    public class ScanConfirmationViewModel : MViewModel
    {
        public ScanConfirmationViewModel(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            RejectScan = new RejectScanCommand(this);
            ConfirmScan = new ConfirmScanCommand(this);
        }

        private string _stockId;
        private IStockRepository _stockRepository;

        public void Init(StockIdParameter parameter)
        {
            StockId = parameter.StockId;

            try
            {
                var allStockItems = _stockRepository.Query("select StockCode, BarCode, StockDescription, StockQuantity, DateUpdated from StockItem");

                var stockItems = _stockRepository.Query("select StockCode, BarCode, StockDescription, StockQuantity, DateUpdated from StockItem where StockCode = ? OR BarCode = ?", StockId, StockId);

                if (stockItems.Any())
                {
                    CurrentStockItem = stockItems.First();
                    StockDescription = CurrentStockItem.StockDescription;
                }
                else
                {
                    //Stock Item is not available..
                }
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
               

        public IMCommand RejectScan { get; set; }
        public IMCommand ConfirmScan { get; set; }

        public string StockId
        {
            get
            {
                return _stockId;
            }

            set
            {
                _stockId = value;
            }
        }

        public StockItem CurrentStockItem { get; private set; }
        public string StockDescription { get; private set; }
    }
}
