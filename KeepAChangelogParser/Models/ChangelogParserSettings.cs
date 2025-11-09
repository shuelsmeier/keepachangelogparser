using System.Collections.ObjectModel;

namespace KeepAChangelogParser.Models
{
  /// <summary>
  /// The changelog parser settings.
  /// </summary>
  public class ChangelogParserSettings
  {
    /// <summary>
    /// The list handling the parser uses.
    /// </summary>
    public ChangelogListHandling ChangelogListHandling { get; set; } = ChangelogListHandling.None;

    /// <summary>
    /// The type of version the parser should check for (default: semantic version).
    /// </summary>
    public ChangelogVersionType ChangelogVersionType { get; set; } = ChangelogVersionType.SemanticVersion;

    /// <summary>
    /// The collection of custom changelog sub section types (default: empty list).
    /// </summary>
#if NET6_0_OR_GREATER
    public Collection<string> CustomChangelogSubSectionTypeCollection { get; init; } = new Collection<string>();
#else
    public Collection<string> CustomChangelogSubSectionTypeCollection { get; } = new Collection<string>();
#endif
  }
}
