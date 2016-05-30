using Android.App;
using Android.OS;
using AndroidHUD;
using MvvmCross.Droid.Views;
using NaaStockScanner.Core.ViewModels;
using NaaStockScanner.Droid._base;

namespace NaaStockScanner.Droid.Views
{
    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);
        }
    }

    [Activity(Label = "View for ReadyToScanViewModel")]
    public class ReadyToScanView : MActivity<ReadyToScanViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ReadyToScanView);
            ViewModel.Context = this;
        }
    }

    [Activity(Label = "View for ScanConfirmationView")]
    public class ScanConfirmationView : MActivity<ScanConfirmationViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ScanConfirmationView);
            ViewModel.Context = this;
        }        
    }

    [Activity(Label = "View for CaptureStockQuantityView")]
    public class CaptureStockQuantityView : MActivity<CaptureStockQuantityViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CaptureStockQuantityView);
            ViewModel.Context = this;
        }
    }
}
