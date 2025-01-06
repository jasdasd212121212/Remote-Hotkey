using System;
using System.Windows.Input;

namespace Remote_Hub.ViewModels
{
    public class Button : ICommand
    {
        public event Action clicked;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool Interacteble = true;

        public bool CanExecute(object parameter)
        {
            return Interacteble;
        }

        public void Execute(object parameter)
        {
            clicked?.Invoke();
        }
    }
}