namespace KeepAChangelogParser.Models
{

  public class ChangelogSection
  {

    public string MarkdownVersion { get; set; } = string.Empty;

    public string MarkdownDate { get; set; } = string.Empty;

    public ChangelogSubSectionCollection SubSectionCollection { get; } = new ChangelogSubSectionCollection();

  }

}