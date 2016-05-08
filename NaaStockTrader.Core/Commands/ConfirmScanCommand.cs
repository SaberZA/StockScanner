using System;
using NaaStockTrader.Core._base;
using NaaStockTrader.Core.Interfaces;

namespace NaaStockTrader.Core.ViewModels
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
                    StockQuantity = _senderViewModel.CurrentStockItem.StockQuantity
                }
            );
        }
    }
}