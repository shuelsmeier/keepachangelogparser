namespace KeepAChangelogParser.Wpf.SampleApp.Models.ReleaseNotesWindow
{

  public class ReleaseNotesDetail
  {

    public string MarkdownTitle { get; set; } = string.Empty;

    public string MarkdownText { get; set; } = string.Empty;

    public ReleaseNotesDetailSectionCollection SectionCollection { get; } = new ReleaseNotesDetailSectionCollection();

  }

}
