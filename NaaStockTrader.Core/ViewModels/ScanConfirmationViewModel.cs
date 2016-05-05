using MvvmCross.Core.ViewModels;
using NaaStockTrader.Core._base;
using NaaStockTrader.Core.Interfaces;
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
        public ScanConfirmationViewModel(IStockRepository stockRepository, ISQLiteConnection sqLiteConnection)
        {
            _stockRepository = stockRepository;
            _sqLiteConnection = sqLiteConnection;
            CheckDatabase = new MvxCommand(() =>
            {
                try
                {
                    var connection = _sqLiteConnection.GetConnection();
                    var rowsInserted = connection.Insert(new StockItem() { StockCode = _stockId, BarCode = "2384576234", StockDescription = "First Stock Item", DateUpdated = DateTime.Now });

                    var stockItem = connection.Query<StockItem>("select StockCode, BarCode, StockDescription, DateUpdated from StockItem where StockCode = ?", _stockId);
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
        private ISQLiteConnection _sqLiteConnection;

        public void Init(StockIdParameter parameter)
        {
            _stockId = parameter.StockId;            
        }

        public MvxCommand CheckDatabase { get; set; }


    }
}
