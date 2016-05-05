using MvvmCross.Core.ViewModels;

namespace NaaStockTrader.Core.ViewModels
{
    public class FirstViewModel 
        : MViewModel
    {
        private string _hello = "Hello MvvmCross";
        public string Hello
        { 
            get { return _hello; }
            set { SetProperty (ref _hello, value); }
        }
    }

    public class ScanConfirmationViewModel : MViewModel
    {

    }

    public class CaptureStockQuantityViewModel : MViewModel
    {

    }

    public class MissingBarcodeViewModel : MViewModel
    {

    }
}
