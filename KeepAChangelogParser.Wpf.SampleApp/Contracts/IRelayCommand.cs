using System;

namespace KeepAChangelogParser.Wpf.SampleApp.Contracts
{
  public interface IRelayCommand
  {
    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter);

    public void Execute(object? parameter);
  }
}
