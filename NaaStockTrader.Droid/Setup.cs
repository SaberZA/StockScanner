using Android.Content;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.Platform;
using NaaStockTrader.Droid.Services.Sqlite;
using MvvmCross.Platform;
using NaaStockTrader.Core.Services.Sql;

namespace NaaStockTrader.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        protected override void InitializeIoC()
        {
            base.InitializeIoC();

            SQLiteConnectionAndroid sqLiteConnectionAndroid = CreateSQLiteConnection();

            Mvx.RegisterType<ISQLiteConnection>(() => sqLiteConnectionAndroid);
            Mvx.RegisterType<IStockRepository>(() => new StockRepository(sqLiteConnectionAndroid));
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
