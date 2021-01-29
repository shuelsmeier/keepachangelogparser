namespace KeepAChangelogParser.Models
{

  public class Changelog
  {

    public string MarkdownTitle { get; set; } = string.Empty;

    public string MarkdownText { get; set; } = string.Empty;

    public ChangelogSectionUnreleased SectionUnreleased { get; } = new ChangelogSectionUnreleased();

    public ChangelogSectionCollection SectionCollection { get; } = new ChangelogSectionCollection();

  }

}
