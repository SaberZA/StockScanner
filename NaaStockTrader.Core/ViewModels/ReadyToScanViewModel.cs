using MvvmCross.Core.ViewModels;
using NaaStockTrader.Core;
using NaaStockTrader.Core._base;
using NaaStockTrader.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NaaStockTrader.Core.ViewModels
{
    public class ReadyToScanViewModel : MViewModel
    {
        public ReadyToScanViewModel()
        {
            ScanComplete = new ScanCompleteCommand(this);
        }

        private string _stockId = "";
        public string StockId
        {
            get { return _stockId; }
            set
            {
                if (value.Contains(Environment.NewLine))    
                {
                    ScanComplete.Execute(new StockIdParameter() { StockId = _stockId });
                }
                else
                {
                    SetProperty(ref _stockId, value);
                }                
            }
        }

        public IMCommand ScanComplete { get; set; }
    }   


    //internal delegate void TimerCallback(object state);

    //internal sealed class Timer : CancellationTokenSource, IDisposable
    //{
    //    public Timer(TimerCallback callback, object state, int dueTime, int period)
    //    {
    //        Task.Delay(dueTime, Token).ContinueWith(async (t, s) =>
    //        {
    //            var tuple = (Tuple<TimerCallback, object>)s;

    //            while (true)
    //            {
    //                if (IsCancellationRequested)
    //                    break;
    //                Task.Run(() => tuple.Item1(tuple.Item2));
    //                await Task.Delay(period);
    //            }

    //        }, Tuple.Create(callback, state), CancellationToken.None,
    //            TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnRanToCompletion,
    //            TaskScheduler.Default);
    //    }

    //    public new void Dispose() { base.Cancel(); }
    //}
}
