using System;
using NaaStockTrader.Core._base;

namespace NaaStockTrader.Core.ViewModels
{
    internal class CorrectQuantityCommand : MCommand
    {
        private CaptureStockQuantityViewModel captureStockQuantityViewModel;

        public CorrectQuantityCommand(CaptureStockQuantityViewModel captureStockQuantityViewModel)
        {
            this.captureStockQuantityViewModel = captureStockQuantityViewModel;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            captureStockQuantityViewModel.Quantity = captureStockQuantityViewModel.Quantity.Substring(0, captureStockQuantityViewModel.Quantity.Length - 1);
        }
    }
}