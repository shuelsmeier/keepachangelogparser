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
    public ChangelogSubSectionItemCollection ItemCollection { get; } = new ChangelogSubSectionItemCollection();

  }
}
