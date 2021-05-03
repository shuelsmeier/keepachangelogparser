namespace KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow
{

  public interface IReleaseNotesWindowView
  {

    object DataContext { get; set; }

    void Close();

    void Hide();

    void InitializeComponent();

    bool? ShowDialog();

  }

}
