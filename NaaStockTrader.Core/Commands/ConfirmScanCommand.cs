using System;
using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Interfaces;

namespace NaaStockScanner.Core.ViewModels
{
    public class ConfirmScanCommand : MCommand
    {
        private ScanConfirmationViewModel _senderViewModel;

        public ConfirmScanCommand(ScanConfirmationViewModel scanConfirmationViewModel)
        {
            this._senderViewModel = scanConfirmationViewModel;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _senderViewModel.ShowViewModel<CaptureStockQuantityViewModel>(
                new StockIdParameter() {
                    StockId = _senderViewModel.StockId,
                    StockDescription = _senderViewModel.CurrentStockItem.StockDescription,
                    StockQuantity = _senderViewModel.CurrentStockItem.StockQuantity,
                    StockPrice = _senderViewModel.CurrentStockItem.StockPrice
                }
            );
        }
    }
}