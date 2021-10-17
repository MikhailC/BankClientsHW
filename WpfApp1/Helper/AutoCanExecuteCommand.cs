using System;
using System.Windows.Input;

namespace WpfApp1.Helper
{
    public class AutoCanExecuteCommand : ICommand
    {
        public ICommand WrappedCommand { get; private set; }

        public AutoCanExecuteCommand(ICommand wrappedCommand)
        {
            WrappedCommand = wrappedCommand ?? throw new ArgumentNullException("wrappedCommand");
        }

        public void Execute(object parameter)
        {
            WrappedCommand.Execute(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return WrappedCommand.CanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}