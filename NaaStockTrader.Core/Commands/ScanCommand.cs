using System;
using MvvmCross.Core.ViewModels;

namespace NaaStockTrader.Core.ViewModels
{
    public class ScanCommand : IMvxCommand
    {
        private MViewModel _viewModel;

        public ScanCommand(MViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute()
        {
            return true;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute()
        {
            _viewModel.ShowViewModel<ReadyToScanViewModel>(null);
        }

        public void Execute(object parameter)
        {
            this.Execute();
        }

        public void RaiseCanExecuteChanged()
        {
            throw new NotImplementedException();
        }
    }
}