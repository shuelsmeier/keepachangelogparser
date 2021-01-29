namespace KeepAChangelogParser.Models
{

  public class ChangelogSubSection
  {

    public ChangelogSubSectionType Type { get; set; }

    public ChangelogSubSectionItemCollection ItemCollection { get; } = new ChangelogSubSectionItemCollection();

  }

}
