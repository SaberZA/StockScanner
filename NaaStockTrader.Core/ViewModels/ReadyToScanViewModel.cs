using MvvmCross.Core.ViewModels;
using NaaStockScanner.Core;
using NaaStockScanner.Core._base;
using NaaStockScanner.Core.Interfaces;
using NaaStockScanner.Core.Services.Sql;
using NaaStockTrader.Core.Services.ExportData;
using NaaStockTrader.Core.Services.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NaaStockScanner.Core.ViewModels
{
    public class ReadyToScanViewModel : MViewModel, IContext
    {
        public ReadyToScanViewModel(IExportDataService exportDataService, IStockRepository stockRepository, IKeyboardService keyboardService)
        {
            ScanComplete = new ScanCompleteCommand(this);
            ExportData = new ExportDataCommand(this, exportDataService, stockRepository);
            ShowKeyboard = new ShowKeyboardCommand(this, keyboardService);
        }

        public void Init()
        {
            _stockId = "";
            SetProperty(ref _stockId, "");
        }

        private string _stockId = "";
        public string StockId
        {
            get { return _stockId; }
            set
            {
                if (value.Contains(Environment.NewLine))    
                {
                    if (_stockId == "")
                    {
                        _stockId = value.Replace(Environment.NewLine, "");
                    }

                    if (!string.IsNullOrEmpty(_stockId))
                    {
                        ScanComplete.Execute(new StockIdParameter() { StockId = _stockId });
                    }
                    
                }
                else
                {
                    SetProperty(ref _stockId, value);
                }                
            }
        }

        public IMCommand ScanComplete { get; set; }
        public IMCommand ExportData { get; set; }
        public IMCommand ShowKeyboard { get; set; }

        public dynamic Context
        {
            get; set;
        }
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
