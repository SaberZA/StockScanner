using System;
using NaaStockScanner.Core._base;

namespace NaaStockScanner.Core.ViewModels
{
    public class RejectScanCommand : MCommand
    {
        private IMViewModel _senderViewModel;

        public RejectScanCommand(IMViewModel scanConfirmationViewModel)
        {
            this._senderViewModel = scanConfirmationViewModel;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _senderViewModel.ShowViewModel<ReadyToScanViewModel>(null);
        }
    }
}