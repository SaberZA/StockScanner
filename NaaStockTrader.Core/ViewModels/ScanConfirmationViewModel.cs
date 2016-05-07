using MvvmCross.Core.ViewModels;
using NaaStockTrader.Core._base;
using NaaStockTrader.Core.Interfaces;
using NaaStockTrader.Core.Services.Csv;
using NaaStockTrader.Core.Services.Sql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Core.ViewModels
{
    public class ScanConfirmationViewModel : MViewModel
    {
        public ScanConfirmationViewModel(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
            //_csvService = csvService;

            CheckDatabase = new MvxCommand(() =>
            {
                try
                {
                    //var rowsInserted = stockRepository.Insert(new StockItem() { StockCode = _stockId, BarCode = "2384576234", StockDescription = "First Stock Item", DateUpdated = DateTime.Now });

                    var allStockItems = stockRepository.Query("select StockCode, BarCode, StockDescription, DateUpdated from StockItem");

                    var stockItem = stockRepository.Query("select StockCode, BarCode, StockDescription, DateUpdated from StockItem where StockCode = ? OR BarCode = ?", _stockId, _stockId);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    throw;
                }
            });
        }

        private string _stockId;
        private IStockRepository _stockRepository;

        public void Init(StockIdParameter parameter)
        {
            _stockId = parameter.StockId;
        }

        public MvxCommand CheckDatabase { get; set; }


    }
}
