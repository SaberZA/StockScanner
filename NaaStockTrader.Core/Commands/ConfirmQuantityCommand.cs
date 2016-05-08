using System;
using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Services.Sql;

namespace NaaStockScanner.Core.ViewModels
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
                Int32.Parse(string.IsNullOrEmpty(captureStockQuantityViewModel.Quantity) ? "0" : captureStockQuantityViewModel.Quantity), 
                captureStockQuantityViewModel.StockId, 
                captureStockQuantityViewModel.StockId);

            captureStockQuantityViewModel.ShowViewModel<ReadyToScanViewModel>(null);
        }
    }
}