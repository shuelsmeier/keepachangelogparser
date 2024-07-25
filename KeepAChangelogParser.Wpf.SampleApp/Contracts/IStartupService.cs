using System.Windows;

namespace KeepAChangelogParser.Wpf.SampleApp.Contracts
{
  public interface IStartupService
  {
    void Execute(
      StartupEventArgs startupEventArgs
    );
  }
}