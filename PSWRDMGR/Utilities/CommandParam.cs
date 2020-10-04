using System;
using System.Windows.Input;

namespace PSWRDMGR.Utilities
{
    /// <summary>
    /// A command capiable of passing parameters and capiable of being executable or not
    /// </summary>
    /// <typeparam name="Parameter">The parameter type (<see cref="string"/>, <see cref="int"/>, etc)</typeparam>
    public class CommandParam<Parameter> : ICommand
    {
        readonly Action<Parameter> _execute;
        readonly Predicate<Parameter> _canExecute;

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The method to execute</param>
        public CommandParam(Action<Parameter> execute) : this(execute, null) { }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The method to execute</param>
        /// <param name="canExecute">can execute or not</param>
        public CommandParam(Action<Parameter> execute, Predicate<Parameter> canExecute)
        {
            if (execute == null)
                return;

            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter)
        {
            if (parameter is Parameter p)
                _execute?.Invoke(p);
        }

        public bool CanExecute(object parameter)
        {
            if (parameter is Parameter p)
                return _canExecute == null ? true : _canExecute(p);
            return false;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
