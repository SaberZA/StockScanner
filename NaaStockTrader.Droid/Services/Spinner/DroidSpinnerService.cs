using System;
using Android.Content;
using AndroidHUD;
using NaaStockTrader.Core.Services.Spinner;

namespace NaaStockScanner.Droid.Services.Spinner
{
    public class DroidSpinnerService : ISpinner
    {
        private bool _isVisible;

        public void ShowSpinner(string message)
        {
            if (Context == null)
            {
                return;
            }
            
            AndHUD.Shared.Show(Context as Context, message, -1, MaskType.Black, null);
            _isVisible = true;
        }

        public void HideSpinner()
        {
            AndHUD.Shared.Dismiss();
            _isVisible = false;
        }

        public void ShowComplete(string message, int seconds = 2)
        {
            AndHUD.Shared.ShowSuccess(Context, message, MaskType.Black, new TimeSpan(0, 0, 0, seconds), HideSpinner);
            _isVisible = true;
        }
        public void ShowError(string message, int seconds = 2)
        {
            AndHUD.Shared.ShowError(Context, message, MaskType.Black, new TimeSpan(0, 0, 0, seconds), HideSpinner);
            _isVisible = true;
        }

        public bool IsVisible
        {
            get { return _isVisible; }
        }

        public void SetContext(dynamic context)
        {
            this.Context = (context as Context);
        }

        public Context Context { get; set; }

    }
}