using System;
using NaaStockTrader.Core._base;
using NaaStockTrader.Core.Services.Sql;

namespace NaaStockTrader.Core.ViewModels
{
    public class ConfirmQuantityCommand : MCommand
    {
        private CaptureStockQuantityViewModel captureStockQuantityViewModel;
        private IStockRepository stockRepository;
        
        public ConfirmQuantityCommand(CaptureStockQuantityViewModel captureStockQuantityViewModel, IStockRepository stockRepository)
        {
            this.captureStockQuantityViewModel = captureStockQuantityViewModel;
            this.stockRepository = stockRepository;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            stockRepository.Execute("Update StockItem Set StockQuantity = ? Where StockCode = ? Or BarCode = ?", 
                Int32.Parse(captureStockQuantityViewModel.Quantity), 
                captureStockQuantityViewModel.StockId, 
                captureStockQuantityViewModel.StockId);

            captureStockQuantityViewModel.ShowViewModel<ReadyToScanViewModel>(null);
        }
    }
}