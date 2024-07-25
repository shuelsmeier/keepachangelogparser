using KeepAChangelogParser.Models;

namespace KeepAChangelogParser.Wpf.SampleApp.Models.ReleaseNotesWindow
{
  public class ReleaseNotesDetailSubSection
  {
    public ChangelogSubSectionType Type { get; internal set; }

    public string MarkdownTitle { get; set; } = string.Empty;

    public string MarkdownText { get; set; } = string.Empty;
  }
}
