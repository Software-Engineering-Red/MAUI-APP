using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UndacApp.Commands
{
    public class ReportNotificationCommand : ICommand
    {
        private readonly Action _execute;

        public event EventHandler CanExecuteChanged;

        public ReportNotificationCommand(Action executeAction)
        {
            _execute = executeAction ?? throw new ArgumentNullException(nameof(executeAction));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute?.Invoke();
        }
    }
}
