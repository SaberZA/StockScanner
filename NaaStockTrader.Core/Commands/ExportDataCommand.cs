using System;
using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Services.Sql;
using NaaStockTrader.Core.Services.ExportData;
using NaaStockTrader.Core.Services.Spinner;
using System.Threading.Tasks;
using System.Diagnostics;
using NaaStockTrader.Core.Services.Dialog;

namespace NaaStockScanner.Core.ViewModels
{
    public class ExportDataCommand : MCommand
    {
        private IExportDataService exportDataService;
        private ReadyToScanViewModel readyToScanViewModel;
        private IStockRepository stockRepository;
        private IDialogService _dialogService;
        private ISpinner _spinnerService;

        public ExportDataCommand(
            ReadyToScanViewModel readyToScanViewModel, 
            IExportDataService exportDataService, 
            IStockRepository stockRepository,
            ISpinner spinnerService,
            IDialogService dialogService)
        {
            this.readyToScanViewModel = readyToScanViewModel;
            this.exportDataService = exportDataService;
            this.stockRepository = stockRepository;
            _spinnerService = spinnerService;
            _dialogService = dialogService;

        }

        public override bool CanExecute()
        {
            return true;
        }

        private Action ExportDataAction;

        public async override void Execute(object parameter)
        {
            ExportDataAction = async () =>
            {
                _spinnerService.ShowSpinner("Exporting Data");
                await Task.Run(() =>
                {
                    try
                    {
                        var stockItems = stockRepository.Query("Select StockCode,BarCode,StockDescription,StockQuantity,DateUpdated from StockItem WHERE StockQuantity > 0");
                        exportDataService.SaveCsvToDevice(stockItems);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                        _spinnerService.HideSpinner();
                        throw ex;
                    }
                    _spinnerService.HideSpinner();
                });
            };

            _dialogService.ShowYesNoDialog("Are you sure you want to export data?", "Export Data", ExportDataAction);


            
        }

        

    }
}