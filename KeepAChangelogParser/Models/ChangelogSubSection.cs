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
#if NET6_0_OR_GREATER
    public ChangelogSubSectionItemCollection ItemCollection { get; init; } = new ChangelogSubSectionItemCollection();
#else
    public ChangelogSubSectionItemCollection ItemCollection { get; } = new ChangelogSubSectionItemCollection();
#endif

    /// <summary>
    /// Changelog sub section type if type is custom
    /// </summary>
    public string? CustomType { get; set; } = null;
  }
}
