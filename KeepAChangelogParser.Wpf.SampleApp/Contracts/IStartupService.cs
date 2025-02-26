using System.Windows;

namespace KeepAChangelogParser.Wpf.SampleApp.Contracts
{
  public interface IStartupService
  {
    public void Execute(
      StartupEventArgs startupEventArgs
    );
  }
}