namespace KeepAChangelogParser.Models
{

  public class ChangelogSectionUnreleased
  {

    public string MarkdownTitle { get; set; } = string.Empty;

    public ChangelogSubSectionCollection SubSectionCollection { get; } = new ChangelogSubSectionCollection();

  }

}
