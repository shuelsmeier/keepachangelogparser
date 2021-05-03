using Ardalis.GuardClauses;
using KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow;

namespace KeepAChangelogParser.Wpf.SampleApp.Commands.ReleaseNotesWindow
{

  public class ReleaseNotesWindowClickOkCommand :
    IReleaseNotesWindowClickOkCommand
  {

    private readonly IReleaseNotesWindowView releaseNotesWindow;

    public ReleaseNotesWindowClickOkCommand(
      IReleaseNotesWindowView releaseNotesWindow
    )
    {
      this.releaseNotesWindow = releaseNotesWindow;
    }

    public void ExecuteClick(
      IReleaseNotesWindowViewModel releaseNotesWindowViewModel
    )
    {
      Guard.Against.Null(releaseNotesWindowViewModel, nameof(releaseNotesWindowViewModel));

      this.releaseNotesWindow.Close();
    }

  }

}
