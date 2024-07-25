namespace KeepAChangelogParser.Models
{
  /// <summary>
  /// Represents a changelog section
  /// </summary>
  public class ChangelogSection
  {
    /// <summary>
    /// Gets or sets the markdown version.
    /// </summary>
    /// <value>
    /// The markdown version.
    /// </value>
    public string MarkdownVersion { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the markdown date.
    /// </summary>
    /// <value>
    /// The markdown date.
    /// </value>
    public string MarkdownDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets the sub section collection.
    /// </summary>
    /// <value>
    /// The sub section collection.
    /// </value>
    public ChangelogSubSectionCollection SubSectionCollection { get; } = new ChangelogSubSectionCollection();
  }
}
