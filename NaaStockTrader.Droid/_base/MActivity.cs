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
using MvvmCross.Droid.Views;
using NaaStockScanner.Core;

namespace NaaStockScanner.Droid._base
{
    public abstract class MActivity<T> : MvxActivity<T> where T : MViewModel
    {
        protected MActivity() { }

        protected override void OnStart()
        {
            ViewModel.PreStartCommand.Execute(this);
            base.OnStart();
            ViewModel.PostStartCommand.Execute(this);
        }
    }
}
