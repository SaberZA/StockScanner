﻿using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Services.Sql;
using NaaStockScanner.Core.ViewModels;
using NaaStockTrader.Core.Services.Spinner;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Core.Commands
{
    public class ScanItemCommand : MCommand
    {
        private ScanConfirmationViewModel scanConfirmationViewModel;
        private ISpinner spinnerService;
        private IStockRepository _stockRepository;

        public ScanItemCommand(
            ScanConfirmationViewModel scanConfirmationViewModel,
            ISpinner spinnerService,
            IStockRepository stockRepository)
        {
            this.scanConfirmationViewModel = scanConfirmationViewModel;
            this.spinnerService = spinnerService;
            _stockRepository = stockRepository;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public async override void Execute(object parameter)
        {
            //scanConfirmationViewModel.StockId = parameter.StockId;
            spinnerService.ShowSpinner("Scanning Stock Item...");
            await Task.Run(() =>
            {
                try
                {
                    //var allStockItems = _stockRepository.Query("select StockCode, BarCode, StockDescription, StockQuantity, DateUpdated from StockItem");
                    //Java.Lang.Thread.Sleep(2000);
                    var stockItems = _stockRepository.Query("select StockCode, BarCode, StockDescription, StockPrice, StockQuantity, DateUpdated from StockItem where StockCode = ? OR BarCode = ?", scanConfirmationViewModel.StockId, scanConfirmationViewModel.StockId);

                    if (stockItems.Any())
                    {
                        scanConfirmationViewModel.CurrentStockItem = stockItems.First();
                        //scanConfirmationViewModel.StockDescription = scanConfirmationViewModel.CurrentStockItem.StockDescription;
                    }
                    else
                    {
                        //Stock Item is not available..
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    spinnerService.HideSpinner();
                    throw;
                }
                spinnerService.HideSpinner();
            });

        }
    }
}
