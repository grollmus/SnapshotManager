using System;
using System.Windows.Input;

namespace SnapshotManager.UI.Mvvm;

public class DelegateCommand : DelegateCommand<object>
{
    /// <inheritdoc />
    public DelegateCommand(Action executeAction, Func<bool> canExecuteFunc = null)
        : base(p => executeAction(), p => canExecuteFunc?.Invoke() ?? true)
    {
    }
}

public class DelegateCommand<TParameter> : ICommand
{
    private readonly Func<TParameter, bool> _canExecuteFunc;
    private readonly Action<TParameter> _executeAction;

    public DelegateCommand(Action<TParameter> executeAction, Func<TParameter, bool> canExecuteFunc = null)
    {
        _executeAction = executeAction;
        _canExecuteFunc = canExecuteFunc ?? (p => true);
    }

    public bool CanExecute(object parameter)
    {
        return _canExecuteFunc((TParameter)parameter);
    }

    public void Execute(object parameter)
    {
        _executeAction((TParameter)parameter);
    }

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler CanExecuteChanged;
}