using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaaStockTrader.Core.Services.Dialog
{
    public interface IDialogService
    {
        /// <summary>
        /// Show a dimple dialog with a message, title, and confirmation button
        /// </summary>
        /// <param name="message">Message to be displayed in the Dialog</param>
        /// <param name="title">Title of the Dialog</param>
        /// <param name="confirmationButtonText">Text of the confirmation button</param>
        void ShowMessage(string message, string title, string confirmationButtonText);

        void ShowYesNoDialog(string message, string title, Action callback);
        /// <summary>
        /// The current context upon which the dialog will show
        /// </summary>
        dynamic Context { get; set; }
    }
}
