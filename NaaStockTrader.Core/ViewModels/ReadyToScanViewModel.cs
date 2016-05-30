using MvvmCross.Core.ViewModels;
using NaaStockScanner.Core;
using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Interfaces;
using NaaStockScanner.Core.Services.Sql;
using NaaStockTrader.Core.Services.Dialog;
using NaaStockTrader.Core.Services.ExportData;
using NaaStockTrader.Core.Services.Keyboard;
using NaaStockTrader.Core.Services.Spinner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NaaStockScanner.Core.ViewModels
{
    public class ReadyToScanViewModel : MViewModel, IContext
    {
        public ReadyToScanViewModel(
            IExportDataService exportDataService, 
            IStockRepository stockRepository, 
            IKeyboardService keyboardService,
            ISpinner spinnerService,
            IDialogService dialogService)
        {
            ScanComplete = new ScanCompleteCommand(this, spinnerService);
            ExportData = new ExportDataCommand(this, exportDataService, stockRepository, spinnerService, dialogService);
            ShowKeyboard = new ShowKeyboardCommand(this, keyboardService);
            _spinnerService = spinnerService;
            _dialogService = dialogService;
        }

        public void Init()
        {
            _stockId = "";
            SetProperty(ref _stockId, "");
        }

        private string _stockId = "";
        public string StockId
        {
            get { return _stockId; }
            set
            {
                if (value.Contains(Environment.NewLine))    
                {
                    if (_stockId == "")
                    {
                        _stockId = value.Replace(Environment.NewLine, "");
                    }

                    if (!string.IsNullOrEmpty(_stockId))
                    {
                        ScanComplete.Execute(new StockIdParameter() { StockId = _stockId });
                    }
                    
                }
                else
                {
                    SetProperty(ref _stockId, value);
                }                
            }
        }

        public IMCommand ScanComplete { get; set; }
        public IMCommand ExportData { get; set; }
        public IMCommand ShowKeyboard { get; set; }

        
        private ISpinner _spinnerService;

        private dynamic _context;
        private IDialogService _dialogService;

        public dynamic Context
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
                _spinnerService.SetContext(value);
                _dialogService.Context = value;
            }
        }
    }       
}
