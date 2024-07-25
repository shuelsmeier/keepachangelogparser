using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;
using System.Windows;

namespace KeepAChangelogParser.Wpf.SampleApp.Commands.ReleaseNotesWindow
{
  public class ReleaseNotesWindowClosedEventCommand :
    IReleaseNotesWindowClosedEventCommand
  {
    public bool CanExecuteClosed()
    {
      return true;
    }

    public void ExecuteClosed()
    {
      Application.Current.Shutdown();
    }
  }
}
