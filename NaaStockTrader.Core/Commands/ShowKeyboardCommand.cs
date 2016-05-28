using System;
using NaaStockScanner.Core._base;
using NaaStockTrader.Core.Services.Keyboard;

namespace NaaStockScanner.Core.ViewModels
{
    public class ShowKeyboardCommand : MCommand
    {
        private IContext _context;
        private IKeyboardService _keyboardService;

        public ShowKeyboardCommand(IContext context, IKeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
            _context = context;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            _keyboardService.ToggleKeyboard(_context.Context);
        }
    }
}