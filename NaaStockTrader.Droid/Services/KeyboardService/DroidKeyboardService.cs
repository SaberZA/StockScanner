using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NaaStockTrader.Core.Services.Keyboard;
using Android.Views.InputMethods;

namespace NaaStockScanner.Droid.Services.KeyboardService
{
    public class DroidKeyboardService : IKeyboardService
    {
        public void ToggleKeyboard(dynamic context)
        {
            var activity = (context as Activity);

            InputMethodManager imm = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
            imm.ToggleSoftInput(ShowFlags.Forced, 0); 
        }
    }
}