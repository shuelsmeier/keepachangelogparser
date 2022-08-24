using Ardalis.GuardClauses;
using KeepAChangelogParser.Wpf.SampleApp.Services;
using System.Windows;

namespace KeepAChangelogParser.Wpf.SampleApp
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App :
    Application
  {
    protected override void OnStartup(
      StartupEventArgs startupEventArgs
    )
    {
      Guard.Against.Null(startupEventArgs, nameof(startupEventArgs));

      base.OnStartup(startupEventArgs);

      new StartupService().Execute(startupEventArgs);
    }

    protected override void OnExit(
      ExitEventArgs exitEventArgs
    )
    {
      Guard.Against.Null(exitEventArgs, nameof(exitEventArgs));

      base.OnExit(exitEventArgs);
    }
  }
}
