using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace NaaStockTrader.Droid.Views
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
    public class ReadyToScanView : MvxActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ReadyToScanView);
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
}
