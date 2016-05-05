using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Core
{
    public abstract class MViewModel : MvxViewModel
    {
        
        public void ShowViewModel<T>(object paramaters)
            where T : MViewModel
        {
            base.ShowViewModel<T>(paramaters);
        }
    }
}
