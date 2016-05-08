using System;
using MvvmCross.Core.ViewModels;
using NaaStockTrader.Core._base;

namespace NaaStockTrader.Core.ViewModels
{
    public class ScanCompleteCommand : MCommand
    {
        private IMViewModel _viewModel;

        public ScanCompleteCommand(IMViewModel viewModel)
        {
            this._viewModel = viewModel;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _viewModel.ShowViewModel<ScanConfirmationViewModel>(parameter);
        }
    }
}