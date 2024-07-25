namespace KeepAChangelogParser.Wpf.SampleApp.Models.ReleaseNotesWindow
{
  public class ReleaseNotesDetailSection
  {
    public string MarkdownTitle { get; set; } = string.Empty;

    public string MarkdownDate { get; set; } = string.Empty;

    public ReleaseNotesDetailSubSectionCollection SubSectionCollection { get; } = new ReleaseNotesDetailSubSectionCollection();
  }
}
