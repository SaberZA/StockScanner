﻿using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockScanner.Core
{
    public interface IMViewModel
    {
        void RaisePropertyChanged(PropertyChangedEventArgs changedArgs);
        void ShowViewModel<T>(object paramters) where T : MViewModel;
    }

    public abstract class MViewModel : MvxViewModel, IMViewModel
    {
        public IMvxCommand PostStartCommand { get; set; }
        public IMvxCommand PreStartCommand { get; set; }

        protected MViewModel()
        {
            PreStartCommand = new MvxCommand(() => { });
            PostStartCommand = new MvxCommand(() => { });
        }

        public override void RaisePropertyChanged(PropertyChangedEventArgs changedArgs)
        {
            base.RaisePropertyChanged(changedArgs);
        }

        public void ShowViewModel<T>(object paramaters)
            where T : MViewModel
        {
            base.ShowViewModel<T>(paramaters);
        }
    }

}
