namespace KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow
{

  public interface IReleaseNotesWindowClickOkCommand
  {

    public void ExecuteClick(
      IReleaseNotesWindowViewModel releaseNotesWindowViewModel
    );

  }

}
