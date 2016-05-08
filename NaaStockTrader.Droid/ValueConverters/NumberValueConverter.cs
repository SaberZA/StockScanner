using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Platform.Converters;

namespace NaaStockTrader.Droid.ValueConverters
{
    public class NumberValueConverter : MvxValueConverter<string,int>
    {
        protected override int Convert(string value, Type targetType, object parameter, CultureInfo cultureInfo)
        {
            int result = 0;
            var canParse = Int32.TryParse(value, out result);

            return result;
        }
    }
}