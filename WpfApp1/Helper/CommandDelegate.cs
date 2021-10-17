using System;
using System.Windows.Input;

namespace WpfApp1.Helper
{
    public class CommandDelegate: ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        
        public CommandDelegate(Action execute)
        {
            _execute = execute;
             
        } 
        
        public CommandDelegate(Action execute, Func<bool> canExecute = null):this(execute)
        {
            
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}