using System;
using MvvmCross.Platform.IoC;
using NaaStockScanner.Core.Services.Sql;

namespace NaaStockScanner.Core
{
    public class App : MvvmCross.Core.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();              

            RegisterAppStart<ViewModels.ReadyToScanViewModel>();
        }
        
    }
}
