using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Core._base
{
    public abstract class MCommand : IMCommand
    {
        public void RaiseCanExecuteChanged()
        {

        }

        public virtual void Execute()
        {
            Execute(null);
        }
        public abstract void Execute(object parameter);

        public abstract bool CanExecute();
        public virtual bool CanExecute(object parameter)
        {
            return this.CanExecute();
        }

        public event EventHandler CanExecuteChanged;
    }

    public interface IMCommand : IMvxCommand
    {
        new void RaiseCanExecuteChanged();

        new void Execute();
        new void Execute(object parameter);

        new bool CanExecute();
        new bool CanExecute(object parameter);

        new event EventHandler CanExecuteChanged;
    }
}
