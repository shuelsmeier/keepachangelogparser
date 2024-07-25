namespace KeepAChangelogParser.Models
{
  /// <summary>
  /// Represents a keepachangelog changelog.
  /// </summary>
  public class Changelog
  {
    /// <summary>
    /// Gets or sets the markdown title.
    /// </summary>
    /// <value>
    /// The markdown title.
    /// </value>
    public string MarkdownTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the markdown text.
    /// </summary>
    /// <value>
    /// The markdown text.
    /// </value>
    public string MarkdownText { get; set; } = string.Empty;

    /// <summary>
    /// Gets the section unreleased.
    /// </summary>
    /// <value>
    /// The section unreleased.
    /// </value>
    public ChangelogSectionUnreleased SectionUnreleased { get; } = new ChangelogSectionUnreleased();

    /// <summary>
    /// Gets the section collection.
    /// </summary>
    /// <value>
    /// The section collection.
    /// </value>
    public ChangelogSectionCollection SectionCollection { get; } = new ChangelogSectionCollection();
  }
}
