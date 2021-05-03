namespace KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow
{

  public interface IReleaseNotesWindowClosedEventCommand
  {

    public bool CanExecuteClosed();

    public void ExecuteClosed();

  }

}
