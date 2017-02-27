using System;
using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Services.Sql;
using NaaStockTrader.Core.Services.Spinner;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NaaStockScanner.Core.ViewModels
{
    public class ConfirmQuantityCommand : MCommand
    {
        private CaptureStockQuantityViewModel captureStockQuantityViewModel;
        private IStockRepository stockRepository;
        private ISpinner _spinnerService;

        public ConfirmQuantityCommand(
            CaptureStockQuantityViewModel captureStockQuantityViewModel, 
            IStockRepository stockRepository,
            ISpinner spinnerService)
        {
            this.captureStockQuantityViewModel = captureStockQuantityViewModel;
            this.stockRepository = stockRepository;
            _spinnerService = spinnerService;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public async override void Execute(object parameter)
        {

            _spinnerService.ShowSpinner("Saving Quantity");
            await Task.Run(() =>
            {
                try
                {
                    stockRepository.Execute("Update StockItem Set StockQuantity = ? Where upper(StockCode) = ? Or upper(BarCode) = ?",
                    Int32.Parse(string.IsNullOrEmpty(captureStockQuantityViewModel.Quantity) ? "0" : captureStockQuantityViewModel.Quantity),
                    captureStockQuantityViewModel.StockId.ToUpper(),
                    captureStockQuantityViewModel.StockId.ToUpper());
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    _spinnerService.HideSpinner();
                    throw ex;
                }
                _spinnerService.HideSpinner();
            });
                

            captureStockQuantityViewModel.ShowViewModel<ReadyToScanViewModel>(null);
        }
    }
}