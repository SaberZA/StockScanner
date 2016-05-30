using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using NaaStockScanner.Droid.Services.Sqlite;
using MvvmCross.Platform;
using NaaStockScanner.Core.Services.Sql;
using NaaStockScanner.Core.Services.Csv;
using System.IO;
using NaaStockScanner.Core.Interfaces;
using NaaStockTrader.Core.Services.ExportData;
using NaaStockTrader.Core.Services.Keyboard;
using NaaStockScanner.Droid.Services.KeyboardService;
using NaaStockTrader.Core.Services.Spinner;
using NaaStockScanner.Droid.Services.Spinner;
using NaaStockTrader.Core.Services.Dialog;
using NaaStockTrader.Droid.Services.Dialog;

namespace NaaStockScanner.Droid
{
    public class Setup : MvxAndroidSetup
    {
        private string stockContent;
        private DroidDataExportService _exportDataService;

        public Setup(Context applicationContext) 
            : base(applicationContext)
        {
            var stream = applicationContext.Assets.Open("Stock20160530.csv");
            
            using (StreamReader sr = new StreamReader(stream))
            {
                stockContent = sr.ReadToEnd();
            }
            _exportDataService = new DroidDataExportService(applicationContext);
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            SQLiteConnectionAndroid sqLiteConnectionAndroid = CreateSQLiteConnection();

            var csvService = new CoreCsvService(stockContent);
            var stockRepository = new StockRepository(sqLiteConnectionAndroid);
            
            stockRepository.SeedStockItems(csvService.GetRecords());

            Mvx.RegisterSingleton<IStockRepository>(stockRepository);

            Mvx.RegisterType<IKeyboardService>(() => new DroidKeyboardService());
            Mvx.RegisterType<IExportDataService>(() => _exportDataService);
            Mvx.RegisterType<ICsvService>(() => new CoreCsvService(stockContent));
            Mvx.RegisterType<ISQLiteConnection>(() => sqLiteConnectionAndroid);            
            Mvx.RegisterType<ISpinner>(() => new DroidSpinnerService());
            Mvx.RegisterType<IDialogService>(() => new AndroidDialogService());

            
        }

        private SQLiteConnectionAndroid CreateSQLiteConnection()
        {
            return new SQLiteConnectionAndroid();
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}
