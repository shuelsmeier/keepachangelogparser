using Ardalis.GuardClauses;
using KeepAChangelogParser.Wpf.SampleApp.Contracts;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace KeepAChangelogParser.Wpf.SampleApp.Commands
{

  public class RelayCommand :
    IRelayCommand,
    ICommand
  {
    private readonly Action<object?> execute;
    private readonly Predicate<object?>? canExecute;

    public RelayCommand(
      Action<object?> execute
    )
      : this(execute, null)
    {
    }

    public RelayCommand(
      Action<object?> execute,
      Predicate<object?>? canExecute
    )
    {
      Guard.Against.Null(execute, nameof(execute));

      this.execute = execute;
      this.canExecute = canExecute;
    }

    [DebuggerStepThrough]
    public bool CanExecute(
      object? parameter
    )
    {
      return this.canExecute == null
          || this.canExecute(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(
      object? parameter
    )
    {
      this.execute(parameter);
    }

  }

  public class RelayCommand<T> :
    ICommand
  {
    private readonly Action<T?> execute;
    private readonly Predicate<T?>? canExecute;

    public RelayCommand(
      Action<T?> execute
    )
      : this(execute, null)
    {
    }

    public RelayCommand(
      Action<T?> execute,
      Predicate<T?>? canExecute
    )
    {
      Guard.Against.Null(execute, nameof(execute));

      this.execute = execute;
      this.canExecute = canExecute;
    }

    [DebuggerStepThrough]
    public bool CanExecute(
      object? parameter
    )
    {
      return this.canExecute == null
          || this.canExecute((T?)parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(
      object? parameter
    )
    {
      this.execute((T?)parameter);
    }

  }

}
