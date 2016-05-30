using System;

namespace NaaStockTrader.Core.Services.Spinner
{
    public interface ISpinner
    {
        void ShowSpinner(string message);

        void HideSpinner();

        void ShowComplete(string message, int seconds = 2);

        void ShowError(string message, int seconds = 2);

        bool IsVisible { get; }

        void SetContext(dynamic context);
    }
}
