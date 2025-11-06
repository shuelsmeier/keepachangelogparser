namespace KeepAChangelogParser.Models
{
  /// <summary>
  /// Represents a changelog sub section item
  /// </summary>
  public class ChangelogSubSectionItem
  {
    /// <summary>
    /// Gets or sets the markdown text.
    /// </summary>
    /// <value>
    /// The markdown text.
    /// </value>
    public string MarkdownText { get; set; } = string.Empty;

    /// <summary>
    /// List of changelog sub section items
    /// </summary>
#if NET6_0_OR_GREATER
    public ChangelogSubSectionItemCollection ItemCollection { get; init; } = new ChangelogSubSectionItemCollection();
#else
    public ChangelogSubSectionItemCollection ItemCollection { get; } = new ChangelogSubSectionItemCollection();
#endif

  }
}
