using Android.App;
using Android.Content;
using NaaStockTrader.Core.Services.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Droid.Services.Dialog
{
    public class AndroidDialogService : IDialogService
    {
        public void ShowMessage(string message, string title, string confirmationButtonText)
        {
            var builder = new AlertDialog.Builder(Context as Context);

            builder.SetTitle(title)
                .SetMessage(message)
                .SetCancelable(false)
                .SetPositiveButton(confirmationButtonText, (s, e) => { });
            
            builder.Create().Show();
        }

        public void ShowYesNoDialog(string message, string title, Action callback)
        {
            var builder = new AlertDialog.Builder(Context as Context);

            builder.SetTitle(title)
                .SetMessage(message)
                .SetCancelable(false)
                .SetPositiveButton("Yes", (s, e) => { callback(); })
                .SetNegativeButton("No", (s, e) => { });

            builder.Create().Show();
        }

        public dynamic Context { get; set; }
    }
}
