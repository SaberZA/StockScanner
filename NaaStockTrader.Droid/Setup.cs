using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using NaaStockTrader.Droid.Services.Sqlite;
using MvvmCross.Platform;
using NaaStockTrader.Core.Services.Sql;
using NaaStockTrader.Core.Services.Csv;
using System.IO;
using NaaStockTrader.Core.Interfaces;

namespace NaaStockTrader.Droid
{
    public class Setup : MvxAndroidSetup
    {
        private string stockContent;

        public Setup(Context applicationContext) : base(applicationContext)
        {
            var stream = applicationContext.Assets.Open("Stock.csv");
            
            using (StreamReader sr = new StreamReader(stream))
            {
                stockContent = sr.ReadToEnd();
            }            
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

            Mvx.RegisterType<ICsvService>(() => new CoreCsvService(stockContent));
            Mvx.RegisterType<ISQLiteConnection>(() => sqLiteConnectionAndroid);
            Mvx.RegisterType<IStockRepository>(() => stockRepository);            
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
