using System;
using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Services.Sql;
using NaaStockTrader.Core.Services.ExportData;

namespace NaaStockScanner.Core.ViewModels
{
    internal class ExportDataCommand : MCommand
    {
        private IExportDataService exportDataService;
        private ReadyToScanViewModel readyToScanViewModel;
        private IStockRepository stockRepository;
        
        public ExportDataCommand(ReadyToScanViewModel readyToScanViewModel, IExportDataService exportDataService, IStockRepository stockRepository)
        {
            this.readyToScanViewModel = readyToScanViewModel;
            this.exportDataService = exportDataService;
            this.stockRepository = stockRepository;

        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var stockItems = stockRepository.Query("Select * from StockItem");
            exportDataService.SaveCsvToDevice(stockItems);
        }
    }
}