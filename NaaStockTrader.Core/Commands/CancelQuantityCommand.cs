using System;
using NaaStockScanner.Core._base;

namespace NaaStockScanner.Core.ViewModels
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