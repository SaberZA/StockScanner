using System;
using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using NaaStockScanner.Droid.Services.Sqlite;
using MvvmCross.Platform;
using NaaStockScanner.Core.Services.Sql;
using NaaStockScanner.Core.Services.Csv;
using System.IO;
using Android.Content.Res;
using NaaStockScanner.Core.Interfaces;
using NaaStockTrader.Core.Services.ExportData;
using NaaStockTrader.Core.Services.Keyboard;
using NaaStockScanner.Droid.Services.KeyboardService;
using NaaStockTrader.Core.Services.Spinner;
using NaaStockScanner.Droid.Services.Spinner;
using NaaStockTrader.Core.Services.Dialog;
using NaaStockTrader.Droid.Services.Dialog;
using PCLStorage;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using FileAccess = System.IO.FileAccess;

namespace NaaStockScanner.Droid
{
    public class Setup : MvxAndroidSetup
    {
        private string stockContent;
        private DroidDataExportService _exportDataService;
        private string _dbFileName = "27Feb2017.sqlite";


        public Setup(Context applicationContext) 
            : base(applicationContext)
        {
            var dbStream = applicationContext.Assets.Open(_dbFileName);
            var localStorage = FileSystem.Current.LocalStorage;
            var documentsPath = localStorage.Path;
            var databasePath = Path.Combine(documentsPath, _dbFileName);

            if (!File.Exists(databasePath))
            {
                var writeStream = new FileStream(databasePath, FileMode.OpenOrCreate, FileAccess.Write);
                ReadWriteStream(dbStream, writeStream);
            }

            //var platform = new SQLitePlatformAndroid();
            //var connection = new SQLiteConnection(platform, databasePath);


            //using (StreamReader sr = new StreamReader(stream))
            //{
            //    stockContent = sr.ReadToEnd();
            //}
            _exportDataService = new DroidDataExportService(applicationContext);
            
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();
            SQLiteConnectionAndroid sqLiteConnectionAndroid = CreateSQLiteConnection(_dbFileName);

            //var csvService = new CoreCsvService(stockContent);
            var stockRepository = new StockRepository(sqLiteConnectionAndroid);
            //stockRepository.SeedStockItems(csvService.GetRecords());

            Mvx.RegisterSingleton<IStockRepository>(stockRepository);

            Mvx.RegisterType<IKeyboardService>(() => new DroidKeyboardService());
            Mvx.RegisterType<IExportDataService>(() => _exportDataService);
            //Mvx.RegisterType<ICsvService>(() => new CoreCsvService(stockContent));
            Mvx.RegisterType<ISQLiteConnection>(() => sqLiteConnectionAndroid);            
            Mvx.RegisterType<ISpinner>(() => new DroidSpinnerService());
            Mvx.RegisterType<IDialogService>(() => new AndroidDialogService());

            
        }

        // readStream is the stream you need to read
        // writeStream is the stream you want to write to
        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }

        private SQLiteConnectionAndroid CreateSQLiteConnection(string dbFileName)
        {
            return new SQLiteConnectionAndroid(dbFileName);
        }

        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}
