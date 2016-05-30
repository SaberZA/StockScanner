using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Interfaces;
using NaaStockScanner.Core.Services.Sql;
using NaaStockTrader.Core.Services.Spinner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockScanner.Core.ViewModels
{
    public class CaptureStockQuantityViewModel : MViewModel
    {
        private IStockRepository _stockRepository;

        public CaptureStockQuantityViewModel(IStockRepository stockRepository,
            ISpinner spinnerService)
        {
            _stockRepository = stockRepository;
            UpdateQuantity = new UpdateQuantityCommand(this);
            CorrectQuantity = new CorrectQuantityCommand(this);

            ConfirmQuantity = new ConfirmQuantityCommand(this, stockRepository, spinnerService);
            CancelQuantity = new CancelQuantityCommand(this);
            _spinnerService = spinnerService;
        }

        public string StockDescription { get; private set; }
        public string StockId { get; private set; }

        private string _quantity;
        public string Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                SetProperty(ref _quantity, value);
            }
        }

        private string _stockPrice;
        public string StockPrice
        {
            get
            {
                return _stockPrice;
            }
            set
            {
                SetProperty(ref _stockPrice, value);
            }
        }

        public void Init(StockIdParameter stockIdParameter)
        {
            this.StockId = stockIdParameter.StockId;
            this.StockDescription = stockIdParameter.StockDescription;
            this.StockPrice = stockIdParameter.StockPrice;

            if (stockIdParameter.StockQuantity > 0)
            {
                Quantity = stockIdParameter.StockQuantity.ToString();
            }
        }

        public IMCommand UpdateQuantity { get; set; }
        public IMCommand CorrectQuantity { get; private set; }
        public IMCommand ConfirmQuantity { get; private set; }
        public IMCommand CancelQuantity { get; private set; }

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
