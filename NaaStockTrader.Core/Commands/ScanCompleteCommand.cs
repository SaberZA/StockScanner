using System;
using MvvmCross.Core.ViewModels;
using NaaStockScanner.Core._base;
using NaaStockTrader.Core.Services.Spinner;

namespace NaaStockScanner.Core.ViewModels
{
    public class ScanCompleteCommand : MCommand
    {
        private ISpinner _spinnerService;
        private IMViewModel _viewModel;

        public ScanCompleteCommand(IMViewModel viewModel, ISpinner spinnerService)
        {
            this._viewModel = viewModel;
            _spinnerService = spinnerService;
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