using MvvmCross.Core.ViewModels;

namespace NaaStockScanner.Core.ViewModels
{
    public class FirstViewModel 
        : MViewModel
    {
        public FirstViewModel()
        {
            Scan = new ScanCommand(this);
        }

        private string _hello = "Hello MvvmCross";
        public string Hello
        { 
            get { return _hello; }
            set
            {                
                SetProperty (ref _hello, value);
            }
        }

        public IMvxCommand Scan { get; set; }
    }    
}
