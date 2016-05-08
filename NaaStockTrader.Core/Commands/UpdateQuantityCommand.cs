using System;
using NaaStockTrader.Core._base;

namespace NaaStockTrader.Core.ViewModels
{
    internal class UpdateQuantityCommand : MCommand
    {
        private CaptureStockQuantityViewModel captureStockQuantityViewModel;

        public UpdateQuantityCommand(CaptureStockQuantityViewModel captureStockQuantityViewModel)
        {
            this.captureStockQuantityViewModel = captureStockQuantityViewModel;
        }
        
        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            captureStockQuantityViewModel.Quantity += (string)parameter;
        }
    }
}