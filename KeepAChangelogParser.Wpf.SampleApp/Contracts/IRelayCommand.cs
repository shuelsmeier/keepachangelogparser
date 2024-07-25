using System;

namespace KeepAChangelogParser.Wpf.SampleApp.Contracts
{
  public interface IRelayCommand
  {
    event EventHandler? CanExecuteChanged;

    bool CanExecute(object? parameter);

    void Execute(object? parameter);
  }
}
