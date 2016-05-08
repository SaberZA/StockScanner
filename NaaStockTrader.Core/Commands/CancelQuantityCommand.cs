using System;
using NaaStockTrader.Core._base;

namespace NaaStockTrader.Core.ViewModels
{
    public class CancelQuantityCommand : MCommand
    {
        private CaptureStockQuantityViewModel captureStockQuantityViewModel;

        public CancelQuantityCommand(CaptureStockQuantityViewModel captureStockQuantityViewModel)
        {
            this.captureStockQuantityViewModel = captureStockQuantityViewModel;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            captureStockQuantityViewModel.ShowViewModel<ReadyToScanViewModel>(null);
        }
    }
}