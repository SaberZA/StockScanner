using MvvmCross.Core.ViewModels;
using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Interfaces;
using NaaStockScanner.Core.Services.Csv;
using NaaStockScanner.Core.Services.Sql;
using NaaStockTrader.Core.Commands;
using NaaStockTrader.Core.Services.Spinner;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockScanner.Core.ViewModels
{
    public class ScanConfirmationViewModel : MViewModel
    {
        public ScanConfirmationViewModel(
            IStockRepository stockRepository,
            ISpinner spinnerService)
        {
            _stockRepository = stockRepository;
            RejectScan = new RejectScanCommand(this);
            ConfirmScan = new ConfirmScanCommand(this);
            PostStartCommand = new ScanItemCommand(this, spinnerService, _stockRepository);
            _spinnerService = spinnerService;
        }

        private string _stockId;
        private IStockRepository _stockRepository;
        
        public void Init(StockIdParameter parameter)
        {
            StockId = parameter.StockId;
        }
               

        public IMCommand RejectScan { get; set; }
        public IMCommand ConfirmScan { get; set; }

        public string StockId
        {
            get
            {
                return _stockId;
            }

            set
            {
                _stockId = value;
            }
        }

        private StockItem _currentStockItem;
        public StockItem CurrentStockItem
        {
            get
            {
                return _currentStockItem;
            }
            set
            {
                _currentStockItem = value;
                StockDescription = _currentStockItem.StockDescription;
            }
        }

        private string _stockDescription;
        public string StockDescription
        {
            get
            {
                return _stockDescription;
            }
            set
            {
                SetProperty(ref _stockDescription, value);
            }
        }

        private dynamic _context;
        private ISpinner _spinnerService;

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
            }
        }
        
    }    
}
