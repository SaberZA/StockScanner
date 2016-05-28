using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;
using NaaStockScanner.Core.ViewModels;

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
    public class ReadyToScanView : MvxActivity<ReadyToScanViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ReadyToScanView);
            ViewModel.Context = this;
        }
    }

    [Activity(Label = "View for ScanConfirmationView")]
    public class ScanConfirmationView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ScanConfirmationView);
        }
    }

    [Activity(Label = "View for CaptureStockQuantityView")]
    public class CaptureStockQuantityView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CaptureStockQuantityView);
        }
    }
}
