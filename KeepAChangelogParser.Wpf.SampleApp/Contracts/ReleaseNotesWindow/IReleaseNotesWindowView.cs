namespace KeepAChangelogParser.Wpf.SampleApp.Contracts.ReleaseNotesWindow
{
  public interface IReleaseNotesWindowView
  {
    public object DataContext { get; set; }

    public void Close();

    public void Hide();

    public void InitializeComponent();

    public bool? ShowDialog();
  }
}
