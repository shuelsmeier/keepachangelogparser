namespace KeepAChangelogParser.Models
{

  /// <summary>
  /// Changelog sub section
  /// </summary>
  public class ChangelogSubSection
  {

    /// <summary>
    /// Changelog sub section type
    /// </summary>
    public ChangelogSubSectionType Type { get; set; }

    /// <summary>
    /// List of changelog sub section items
    /// </summary>
    public ChangelogSubSectionItemCollection ItemCollection { get; } = new ChangelogSubSectionItemCollection();

  }

}
