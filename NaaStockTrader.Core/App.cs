using System;
using MvvmCross.Platform.IoC;
using NaaStockTrader.Core.Services.Sql;

namespace NaaStockTrader.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();              

            RegisterAppStart<ViewModels.FirstViewModel>();
        }
        
    }
}
